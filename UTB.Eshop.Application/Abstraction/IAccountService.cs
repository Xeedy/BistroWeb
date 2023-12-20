using System;
using BistroWeb.Application.ViewModels;
using BistroWeb.Infrastructure.Identity.Enums;

namespace BistroWeb.Application.Abstraction
{
    public interface IAccountService
    {
        Task<string[]> Register(RegisterViewModel vm, Roles role);
        Task<bool> Login(LoginViewModel vm);
        Task Logout();
    }
}

