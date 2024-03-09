using System;
using BistroWeb.Application.Abstraction;
using BistroWeb.Application.ViewModels;
using BistroWeb.Infrastructure.Identity.Enums;
using BistroWeb.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace BistroWeb.Application.Implementation
{
    public class AccountIdentityService : IAccountService
    {
        UserManager<User> userManager;
        SignInManager<User> sigInManager;

        public AccountIdentityService(UserManager<User> userManager, SignInManager<User> sigInManager)
        {
            this.userManager = userManager;
            this.sigInManager = sigInManager;
        }

        public async Task<bool> Login(LoginViewModel vm)
        {
            var result = await sigInManager.PasswordSignInAsync(vm.Username, vm.Password, true, true);
            return result.Succeeded;
        }

        public Task Logout()
        {
            return sigInManager.SignOutAsync();
        }

        public async Task<string[]> Register(RegisterViewModel vm, Roles role)
        {
            User user = new User()
            {
                UserName = vm.Username,
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                Email = vm.Email,
                PhoneNumber = vm.Phone
            };

            string[] errors = null;

            var result = await userManager.CreateAsync(user, vm.Password);
            if (result.Succeeded)
            {
                var resultRole = await userManager.AddToRoleAsync(user, role.ToString());

                if (resultRole.Succeeded == false)
                {
                    for (int i = 0; i < result.Errors.Count(); ++i)
                        result.Errors.Append(result.Errors.ElementAt(i));
                }
            }

            if (result.Errors != null && result.Errors.Count() > 0)
            {
                errors = new string[result.Errors.Count()];
                for (int i = 0; i < result.Errors.Count(); ++i)
                {
                    errors[i] = result.Errors.ElementAt(i).Description;
                }
            }

            return errors;
        }
        public async Task<User> GetUserDetailsAsync(string username)
        {
            // Assuming 'User' is your user entity class and it includes the properties needed by UserProfileViewModel
            var user = await userManager.FindByNameAsync(username);
            if (user != null)
            {
                // If you need to transform 'User' entity to a different type before returning, do it here
                // For example, if you need to return a DTO or ViewModel instead of the User entity directly
                // This is just returning the user entity as-is for simplicity
                return user;
            }
            else
            {
                // Handle the case where the user is not found
                // You might want to return null or throw an exception depending on your application's needs
                return null;
            }
        }
        public Task<User> GetCurrentUser(ClaimsPrincipal principal)
        {
            return userManager.GetUserAsync(principal);
        }
        public async Task<IEnumerable<string>> ChangePassword(ChangePasswordViewModel model, User user)
        {
            if (user == null)
            {
                return new List<string> { "User not found." };
            }

            var result = await userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

            if (result.Succeeded)
            {
                return null; // Úspěšně aktualizováno
            }

            return result.Errors.Select(error => error.Description);

        }
    }
}

