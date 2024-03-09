using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BistroWeb.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BistroWeb.Application.ViewModels;

namespace Portal.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AccountsController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;

        public AccountsController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public IActionResult Accounts()
        {
            var users = _userManager.Users.ToList();
            var userViewModels = users.Select(user => new UserListViewModel
            {
                Id = user.Id.ToString(),
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Roles = _userManager.GetRolesAsync(user).Result.ToList()
            }).ToList();

            return View(userViewModels);
        }

        public async Task<IActionResult> DeleteAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Accounts));
            }
            else
            {
                return View("Error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditAsync(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);
            if (user == null)
            {
                return NotFound();
            }

            var allRoles = await _roleManager.Roles.Select(r => r.Name).ToListAsync();
            var userRoles = await _userManager.GetRolesAsync(user);

            var viewModel = new EditUserViewModel
            {
                Id = user.Id.ToString(),
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Roles = allRoles,
                SelectedRoles = userRoles.ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var existing = await _userManager.FindByIdAsync(viewModel.Id);
                if (existing == null)
                {
                    return NotFound();
                }

                // Check if the entered username, email, or phone number is already taken
                var existingUserByName = await _userManager.FindByNameAsync(viewModel.UserName);
                var existingUserByEmail = await _userManager.FindByEmailAsync(viewModel.Email);
                var existingUserByPhoneNumber = await _userManager.FindByNameAsync(viewModel.PhoneNumber);

                if (existingUserByName != null && existingUserByName.Id != existing.Id)
                {
                    ModelState.AddModelError("UserName", "Username is already taken.");
                }

                if (existingUserByEmail != null && existingUserByEmail.Id != existing.Id)
                {
                    ModelState.AddModelError("Email", "Email is already taken.");
                }

                if (existingUserByPhoneNumber != null && existingUserByPhoneNumber.Id != existing.Id)
                {
                    ModelState.AddModelError("PhoneNumber", "Phone number is already taken.");
                }

                if (!ModelState.IsValid)
                {
                    // Repopulate the roles options
                    viewModel.Roles = await _roleManager.Roles.Select(r => r.Name).ToListAsync();
                    return View(viewModel);
                }

                existing.UserName = viewModel.UserName;
                existing.FirstName = viewModel.FirstName;
                existing.LastName = viewModel.LastName;
                existing.Email = viewModel.Email;
                existing.PhoneNumber = viewModel.PhoneNumber;

                var result = await _userManager.UpdateAsync(existing);

                if (result.Succeeded)
                {
                    await UpdateUserRoles(existing, viewModel.SelectedRoles);

                    return RedirectToAction(nameof(Accounts));
                }
                else
                {
                    return View("Error");
                }
            }

            // If ModelState is not valid, repopulate the roles options
            viewModel.Roles = await _roleManager.Roles.Select(r => r.Name).ToListAsync();
            return View(viewModel);
        }



        private async Task UpdateUserRoles(User user, List<string> roles)
        {
            // Získání stávajících rolí uživatele
            var userRoles = await _userManager.GetRolesAsync(user);

            // Odebrání uživatele z rolí, ve kterých není
            var rolesToRemove = userRoles.Except(roles);
            await _userManager.RemoveFromRolesAsync(user, rolesToRemove);

            // Přidání uživatele do nových rolí
            var rolesToAdd = roles.Except(userRoles);
            await _userManager.AddToRolesAsync(user, rolesToAdd);
        }
    }
}
