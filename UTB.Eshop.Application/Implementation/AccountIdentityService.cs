using System;
using Microsoft.AspNetCore.Identity;
using BistroWeb.Application.Abstraction;
using BistroWeb.Application.ViewModels;
using BistroWeb.Infrastructure.Identity;
using BistroWeb.Infrastructure.Identity.Enums;
using BistroWeb.Domain.Entities.Interfaces;

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
        public async Task<IUser> GetCurrentUser()
        {
            // Retrieve the current user from the user manager
            var currentUser = await userManager.GetUserAsync(sigInManager.Context.User);

            return currentUser;
        }
        public async Task<IEnumerable<IUser>> GetAllUsersForRole(Roles role)
        {
            // Retrieve users with the specified role from the database
            var users = await userManager.GetUsersInRoleAsync(role.ToString());

            // Convert the list of User objects to IUser interface
            var userList = users.Cast<IUser>().ToList();

            return userList;
        }
        public async Task<IUser> GetUserById(int id)
        {
            // Retrieve the user by id from the user manager
            var user = await userManager.FindByIdAsync(id.ToString());

            return user;
        }
        public async Task<bool> Edit(User editedUser)
        {
            try
            {
                // Update user information
                await userManager.UpdateAsync(editedUser);
                return true; // Indicate success
            }
            catch (Exception ex)
            {
                // Handle any exceptions or errors here
                // Log the exception if necessary
                return false; // Indicate failure
            }
        }
    }
}

