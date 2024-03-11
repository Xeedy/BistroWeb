using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BistroWeb.Application.Abstraction;
using BistroWeb.Application.ViewModels;
using BistroWeb.Infrastructure.Identity.Enums;
using BistroWeb.Web.Controllers;
using BistroWeb.Domain.Entities;
using BistroWeb.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using BistroWeb.Infrastructure.Database;
using Microsoft.AspNetCore.Identity;

namespace Portal.Web.Areas.Security.Controllers
{
    [Area("Security")]
    public class AccountController : Controller
    {
        IAccountService accountService;
        EshopDbContext _eshopDbContext;
        private readonly UserManager<User> _userManager;

        public AccountController(IAccountService accountService, EshopDbContext eshopDbContext, UserManager<User> userManager)
        {
            this.accountService = accountService;
            this._eshopDbContext = eshopDbContext;
            this._userManager = userManager; // UserManager injection
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerVM)
        {
            if (ModelState.IsValid)
            {
                string[] errors = await accountService.Register(registerVM, Roles.Customer);

                if (errors == null)
                {
                    LoginViewModel loginVM = new LoginViewModel()
                    {
                        Username = registerVM.Username,
                        Password = registerVM.Password
                    };

                    bool isLogged = await accountService.Login(loginVM);
                    if (isLogged)
                        return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).Replace(nameof(Controller), String.Empty), new { area = String.Empty });
                    else
                        return RedirectToAction(nameof(Login));
                }
                else
                {
                    //error to ViewModel
                }

            }

            return View(registerVM);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (ModelState.IsValid)
            {
                bool isLogged = await accountService.Login(loginVM);
                if (isLogged)
                    return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).Replace(nameof(Controller), String.Empty), new { area = String.Empty });

                loginVM.LoginFailed = true;
            }

            return View(loginVM);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await accountService.Logout();
            return RedirectToAction(nameof(Login));
        }
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User); // Get the current user
            if (user == null)
            {
                return NotFound();
            }

            var model = new UserProfileViewModel
            {
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                UpcomingShifts = await _eshopDbContext.Shifts
                    .Where(s => s.UserId == user.Id && s.StartDate >= DateTime.Today) // Use user.Id for filtering
                    .OrderBy(s => s.StartDate)
                    .ToListAsync()
            };

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await accountService.GetCurrentUser(User);
                var result = await accountService.ChangePassword(model, user); // update his odl information with the new one

                if (result == null)
                {
                    // Uložení proběhlo úspěšně, přesměrujte na seznam uživatelů nebo jinam.
                    return RedirectToAction(nameof(Profile));
                }

                // Přidání chybových zpráv do ModelState
                foreach (var error in result)
                {
                    ModelState.AddModelError(string.Empty, error);
                }
            }

            return View(model);
        }
    }
}
