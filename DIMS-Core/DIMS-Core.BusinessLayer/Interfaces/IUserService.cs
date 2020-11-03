using DIMS_Core.BusinessLayer.Models.Account;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace DIMS_Core.BusinessLayer.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<SignInResult> SignIn(SignInModel model);
        Task SignOut();
        Task<IdentityResult> SignUp(SignUpModel model);
    }
}