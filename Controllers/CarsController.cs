using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FriBergs_CarRental.Data;
using FriBergs_CarRental.Models;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;


namespace FriBergs_CarRental.Controllers
{
    public class CarsController : Controller
    {
        private readonly IGenericRepository<Car> _context;
        private readonly IMapper _mapper;

        public CarsController(IGenericRepository<Car> context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: Cars
        public async Task<IActionResult> Index()
        {
            return View(await _context.GetAllAsync());
        }

        [Authorize("Roles = Admin")]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [Authorize("Roles = Admin")]
        public async Task<IActionResult> Create([Bind("Id, Images, Model, Brand")] Car car)
        {
            if (ModelState.IsValid)
            {
                _context.AddAsync(car);
                return RedirectToAction("Index");
            }
            return View(car);
        }

    }
}
