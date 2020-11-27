using DIMS_Core.BusinessLayer.Models.Account;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DIMS_Core.Identity.Entities;

namespace DIMS_Core.BusinessLayer.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<SignInResult> SignIn(SignInModel model);
        Task SignOut();
        Task<IdentityResult> SignUp(SignUpModel model);
        IEnumerable<Role> GetRoles();
        User GetUser(string email);
        Task DeleteUser(User user);
        Task<string> GetUserRole(User user);
        Task AddUserRole(User user, string role);
        Task UpdateUserRole(User user, string role);
        Task DeleteUserRole(User user, string role);
    }
}