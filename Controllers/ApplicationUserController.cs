using AutoMapper;
using FriBergs_CarRental.Data;
using FriBergs_CarRental.Data.Repository;
using FriBergs_CarRental.Models;
using FriBergs_CarRental.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
            List<ApplicationUserIndexViewModel> usersView = _mapper.Map<List<ApplicationUserIndexViewModel>>(users);

            for (int i = 0; i < usersView.Count; i++)
            {
                var roles = await _userManager.GetRolesAsync(users[i]);
                usersView[i].RoleName = roles.FirstOrDefault() ?? "No role";
            }
            return View(usersView);
        }


        public async Task<ActionResult> Details(string id)
        {
            ApplicationUserIndexViewModel userVM = _mapper.Map<ApplicationUserIndexViewModel>(_repo.GetById(id));
            var roles = await _userManager.GetRolesAsync(_repo.GetById(id));
            userVM.RoleName = roles.FirstOrDefault() ?? "No role";
            return View(userVM);
        }


        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName, LastName, Email, RoleName")] ApplicationUserCreateViewModel userView)
        {


            if (ModelState.IsValid)
            {
                ApplicationUser user = _mapper.Map<ApplicationUser>(userView);
                user.UserName = userView.Email;


                var result = await _userManager.CreateAsync(user, "Hej123!");
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, userView.RoleName);
                    return RedirectToAction("Index");

                }

                return View(userView);

            }

            return View(userView);
        }
        public async Task<IActionResult> Edit(string id)
        {
            ApplicationUserIndexViewModel userVM = _mapper.Map<ApplicationUserIndexViewModel>(_repo.GetById(id));
            var roles = await _userManager.GetRolesAsync(_repo.GetById(id));
            userVM.RoleName = roles.FirstOrDefault() ?? "No role";
            return View(userVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id, FirstName, LastName, Email, RoleName")] ApplicationUserIndexViewModel userVM)
        {
            if (ModelState.IsValid)
            {

                ApplicationUser user = await _repo.GetByIdAsync(userVM.Id);
                if (user == null)
                    return NotFound();

                user.FirstName = userVM.FirstName;
                user.LastName = userVM.LastName;
                user.Email = userVM.Email;
                user.UserName = userVM.Email;

                var updateResult = await _userManager.UpdateAsync(user);
                if (updateResult.Succeeded)
                {
                    var currentRoles = await _userManager.GetRolesAsync(user);
                    if (currentRoles.Any())
                    {
                        await _userManager.RemoveFromRolesAsync(user, currentRoles);
                    }

                    await _userManager.AddToRoleAsync(user, userVM.RoleName);
                    return RedirectToAction("Index");
                }

            }
            return View(userVM);
        }


        public async Task<IActionResult> Delete(string id)
        {
            ApplicationUserIndexViewModel userVM = _mapper.Map<ApplicationUserIndexViewModel>(_repo.GetById(id));
            var roles = await _userManager.GetRolesAsync(_repo.GetById(id));
            userVM.RoleName = roles.FirstOrDefault() ?? "No role";
            return View(userVM);
        }



        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            ApplicationUser user = await _repo.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            await _userManager.DeleteAsync(user);
            return RedirectToAction("Index");

        }
    }
}
