using AutoMapper;
using FriBergs_CarRental.Data;
using FriBergs_CarRental.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FriBergs_CarRental.Controllers
{
    [Authorize(Roles = "Admin")]
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


    }
}
