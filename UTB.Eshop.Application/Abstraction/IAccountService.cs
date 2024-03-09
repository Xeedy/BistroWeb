using System.Collections.Generic;
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
        Task<IUser> GetCurrentUser();
        Task<IEnumerable<IUser>> GetAllUsersForRole(Roles role);
        Task<bool> Edit(User editedUser);
        Task<IUser> GetUserById(int id);
    }
}