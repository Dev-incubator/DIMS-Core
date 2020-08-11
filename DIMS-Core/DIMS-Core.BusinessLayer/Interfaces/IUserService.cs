using DIMS_Core.BusinessLayer.Models.Account;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace DIMS_Core.BusinessLayer.Interfaces
{
    public interface IUserService
    {
        Task SignOutAsync();
        Task<SignInResult> SignInAsync(SignInModel model);
        Task<IdentityResult> RegistAsync(UserRegistModel model);
        Task DeleteAsync(int identityId);
        void Dispose();
    }
}