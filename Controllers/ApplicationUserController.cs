using AutoMapper;
using FriBergs_CarRental.Data;
using FriBergs_CarRental.Data.Repository;
using FriBergs_CarRental.Models;
using FriBergs_CarRental.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace FriBergs_CarRental.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ApplicationUserController : Controller
    {
        private readonly IUserRepository _repo;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public ApplicationUserController(IUserRepository repo, IMapper mapper, ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _repo = repo;
            _mapper = mapper;
            _context = context;
            _userManager = userManager;
        }


        public async Task<IActionResult> Index()
        {
            List<ApplicationUser> users = await _repo.GetAllAsync();
            List<ApplicationUserViewModel> usersView = _mapper.Map<List<ApplicationUserViewModel>>(users);

            for (int i = 0; i < usersView.Count; i++)
            {
                var roles = await _userManager.GetRolesAsync(users[i]);
                usersView[i].RoleName = roles.FirstOrDefault() ?? "No role";
            }


            return View(usersView);
        }


        public ActionResult Details(int id)
        {
            return View();
        }

       
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Edit(int id)
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

     
        public ActionResult Delete(int id)
        {
            return View();
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
