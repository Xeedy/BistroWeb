using System;
using Microsoft.AspNetCore.Identity;
using BistroWeb.Domain.Entities.Interfaces;
using BistroWeb.Domain.Entities;

namespace BistroWeb.Infrastructure.Identity
{
    public class User : IdentityUser<int>, IUser
    {
        public virtual string? FirstName { get; set; }
        public virtual string? LastName { get; set; }
    }
}

