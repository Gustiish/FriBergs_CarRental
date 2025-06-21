using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FriBergs_CarRental.Data;
using FriBergs_CarRental.Models;

namespace FriBergs_CarRental.Controllers
{
    public class UserCustomerOrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserCustomerOrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UserCustomerOrders
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CustomerOrder.Include(c => c.ApplicationUser).Include(c => c.Car);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: UserCustomerOrders/Details/5
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

        // GET: UserCustomerOrders/Create
        public IActionResult Create()
        {
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["CarId"] = new SelectList(_context.Car, "Id", "Id");
            return View();
        }

        // POST: UserCustomerOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CarId,StartTime,EndTime,Price,ApplicationUserId")] CustomerOrder customerOrder)
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

        // GET: UserCustomerOrders/Edit/5
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

        // POST: UserCustomerOrders/Edit/5
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

        // GET: UserCustomerOrders/Delete/5
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

        // POST: UserCustomerOrders/Delete/5
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
