using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BistroWeb.Application.ViewModels;
using BistroWeb.Web.Controllers;
using BistroWeb.Application.Abstraction;
using BistroWeb.Infrastructure.Identity.Enums;
using Microsoft.AspNetCore.Authorization;

namespace BistroWeb.Web.Areas.Security.Controllers
{
    [Area("Security")]
    public class AccountController : Controller
    {
        IAccountService accountService;

        public AccountController(IAccountService security)
        {
            this.accountService = security;
        }
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            // Retrieve the current user details from your service or repository
            var user = await accountService.GetCurrentUser();

            // Pass the user details to the view
            return View(user);
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

    }
}
