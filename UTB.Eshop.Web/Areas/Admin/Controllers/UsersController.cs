using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BistroWeb.Application.Abstraction;
using BistroWeb.Infrastructure.Identity.Enums; // Make sure to include the appropriate namespace
using BistroWeb.Infrastructure.Identity;

namespace BistroWeb.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly IAccountService _accountService;

        public UsersController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task<IActionResult> Index()
        {
            var role = Roles.Customer; // Use the enum directly
            var users = await _accountService.GetAllUsersForRole(role);
            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            // Retrieve the user by id
            var user = await _accountService.GetUserById(id);

            // Check if the user exists
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, User editedUser)
        {
            if (id != editedUser.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                // Update user information
                var result = await _accountService.Edit(editedUser);

                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Handle update failure
                    ModelState.AddModelError("", "Failed to update user information.");
                }
            }

            return View(editedUser);
        }
    }
}
