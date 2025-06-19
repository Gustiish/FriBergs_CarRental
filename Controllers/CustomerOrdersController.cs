using FriBergs_CarRental.Data;
using FriBergs_CarRental.Models;
using FriBergs_CarRental.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FriBergs_CarRental.Controllers
{
    public class CustomerOrdersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IGenericRepository<Car> _repoCar;

        public CustomerOrdersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IGenericRepository<Car> repoCar)
        {
            _context = context;
            _userManager = userManager;
            _repoCar = repoCar;
        }

        // GET: CustomerOrders
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CustomerOrder.Include(c => c.ApplicationUser).Include(c => c.Car);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CustomerOrders/Details/5

        public async Task<IActionResult> Details(int? id)
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

        // GET: CustomerOrders/Create
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
            return View(viewModel);
        }

        // POST: CustomerOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Customer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateBooking([Bind("Id,CarId,StartTime,EndTime,Price,ApplicationUserId")] CustomerOrder customerOrder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customerOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id", customerOrder.ApplicationUserId);
            ViewData["CarId"] = new SelectList(_context.Car, "Id", "Id", customerOrder.CarId);
            return View(customerOrder);
        }

        // GET: CustomerOrders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerOrder = await _context.CustomerOrder.FindAsync(id);
            if (customerOrder == null)
            {
                return NotFound();
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id", customerOrder.ApplicationUserId);
            ViewData["CarId"] = new SelectList(_context.Car, "Id", "Id", customerOrder.CarId);
            return View(customerOrder);
        }

        // POST: CustomerOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CarId,StartTime,EndTime,Price,ApplicationUserId")] CustomerOrder customerOrder)
        {
            if (id != customerOrder.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customerOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerOrderExists(customerOrder.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id", customerOrder.ApplicationUserId);
            ViewData["CarId"] = new SelectList(_context.Car, "Id", "Id", customerOrder.CarId);
            return View(customerOrder);
        }

        // GET: CustomerOrders/Delete/5
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

        // POST: CustomerOrders/Delete/5
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
    }
}
