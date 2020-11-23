using DIMS_Core.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using System;

namespace DIMS_Core.Identity.Services
{
    public interface IIdentityUnitOfWork : IDisposable
    {
        SignInManager<User> SignInManager { get; }
        UserManager<User> UserManager { get; }
        RoleManager<Role> RoleManager { get; }
    }
}