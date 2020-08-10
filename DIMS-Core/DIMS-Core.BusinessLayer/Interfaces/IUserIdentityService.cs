using DIMS_Core.BusinessLayer.Models.Account;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace DIMS_Core.BusinessLayer.Interfaces
{
    public interface IUserIdentityService : IDisposable
    {
        Task<SignInResult> SignInAsync(SignInModel model);
        Task SignOutAsync();
        Task<IdentityResult> SignUpAsync(SignUpModel model);
        Task<IdentityResult> DeleteAsync(int Id);
        Task<IdentityResult> DeleteAsync(string Email);
    }
}