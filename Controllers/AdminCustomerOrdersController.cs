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
    public class AdminCustomerOrdersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IGenericRepository<Car> _repoCar;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<CustomerOrder> _repoOrders;

        public AdminCustomerOrdersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IGenericRepository<Car> repoCar, IMapper mapper, IGenericRepository<CustomerOrder> repoOrders)
        {
            _context = context;
            _userManager = userManager;
            _repoCar = repoCar;
            _mapper = mapper;
            _repoOrders = repoOrders;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var orders = _context.CustomerOrder.Include(c => c.ApplicationUser).Include(c => c.Car);
            return View(await orders.ToListAsync());
        }


        public async Task<IActionResult> Details(int id)
        {

            var customerOrder = await _repoOrders.GetByIdAsync(id);
            if (customerOrder == null)
            {
                return NotFound();
            }

            return View(customerOrder);
        }

        [Authorize(Roles = "Customer")]
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

        [Authorize(Roles = "Customer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateBooking([Bind("CarId,Car,StartTime,EndTime,Price")] CreateCustomerOrderViewModel customerOrderVM)
        {
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

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerOrder = await _context.CustomerOrder
                .Include(c => c.ApplicationUser)
                .Include(c => c.Car)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customerOrder == null)
            {
                return NotFound();
            }

            return View(customerOrder);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customerOrder = await _context.CustomerOrder.FindAsync(id);
            if (customerOrder != null)
            {
                _context.CustomerOrder.Remove(customerOrder);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerOrderExists(int id)
        {
            return _context.CustomerOrder.Any(e => e.Id == id);
        }

        public IActionResult ConfirmedBooking(int orderId)
        {
            var order = _context.CustomerOrder.Include(x => x.Car).Include(x => x.ApplicationUser).FirstOrDefault(x => x.Id == orderId);

            if (order == null)
                return NotFound();
            return View(order);
        }
    }
}
