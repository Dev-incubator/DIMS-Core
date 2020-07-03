using DIMS_Core.BusinessLayer.Models.Account;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace DIMS_Core.BusinessLayer.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<SignInResult> SignInAsync(SignInModel model);
        Task SignOutAsync();
        Task<IdentityResult> SignUpAsync(SignUpModel model);
    }
}