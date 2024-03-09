using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using BistroWeb.Application.ViewModels;
using BistroWeb.Domain.Entities.Interfaces;
using BistroWeb.Infrastructure.Identity;
using BistroWeb.Infrastructure.Identity.Enums;

namespace BistroWeb.Application.Abstraction
{
    public interface IAccountService
    {
        Task<string[]> Register(RegisterViewModel vm, Roles role);
        Task<bool> Login(LoginViewModel vm);
        Task Logout();
        Task<User> GetUserDetailsAsync(string username);
        Task<User> GetCurrentUser(ClaimsPrincipal principal);
        Task<IEnumerable<string>> ChangePassword(ChangePasswordViewModel model, User user);

    }
}