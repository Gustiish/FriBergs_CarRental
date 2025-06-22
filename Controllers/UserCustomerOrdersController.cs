using AutoMapper;
using FriBergs_CarRental.Data;
using FriBergs_CarRental.Models;
using FriBergs_CarRental.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FriBergs_CarRental.Controllers
{
    [Authorize(Roles = "Customer")]
    public class UserCustomerOrdersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IGenericRepository<Car> _repoCar;
        private readonly IGenericRepository<CustomerOrder> _repoOrders;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public UserCustomerOrdersController(ApplicationDbContext context, IGenericRepository<Car> repoCar, IGenericRepository<CustomerOrder> repoOrder, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _context = context;
            _repoCar = repoCar;
            _repoOrders = repoOrder;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userOrders = _context.CustomerOrder.Where(x => x.ApplicationUserId == userId);
            return View(userOrders);
        }

        public async Task<IActionResult> CreateBooking(int id)
        {
            CreateCustomerOrderViewModel viewModel = new CreateCustomerOrderViewModel();

            Car car = await _repoCar.GetByIdAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            viewModel.Car = car;
            viewModel.CarId = car.Id;
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateBooking([Bind("CarId,Car,StartTime,EndTime,Price")] CreateCustomerOrderViewModel customerOrderVM)
        {
            if (!IsBookingAvailable(customerOrderVM.CarId, customerOrderVM.StartTime, customerOrderVM.EndTime))
            {
                ViewBag.ErrorMessage = "Dem datumen är redan upptagna.";
                customerOrderVM.Car = await _repoCar.GetByIdAsync(customerOrderVM.CarId);
                return View(customerOrderVM);
            }

            if (ModelState.IsValid)
            {
                CustomerOrder customerOrder = _mapper.Map<CustomerOrder>(customerOrderVM);

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                customerOrder.ApplicationUserId = userId;
                customerOrder.Car = await _repoCar.GetByIdAsync(customerOrderVM.CarId);

                await _repoOrders.AddAsync(customerOrder);

                return RedirectToAction("ConfirmedBooking", new { orderId = customerOrder.Id });

            }

            customerOrderVM.Car = await _repoCar.GetByIdAsync(customerOrderVM.CarId);
            return View(customerOrderVM);

        }

        public IActionResult ConfirmedBooking(int orderId)
        {
            var order = _context.CustomerOrder.Include(x => x.Car).Include(x => x.ApplicationUser).FirstOrDefault(x => x.Id == orderId);

            if (order == null)
                return NotFound();
            return View(order);
        }

        private bool IsBookingAvailable(int carId, DateOnly start, DateOnly end)
        {
            if (_context.CustomerOrder.Any(o => o.CarId == carId && start <= o.EndTime && end >= o.StartTime))
            {
                return false;
            }
            else { return true; }


        }



    }
}
