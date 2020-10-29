using AutoMapper;
using DIMS_Core.BusinessLayer.Interfaces;
using DIMS_Core.BusinessLayer.Models.Account;
using DIMS_Core.Identity.Entities;
using DIMS_Core.Identity.Services;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace DIMS_Core.BusinessLayer.Services
{
    internal class UserService : IUserService
    {
        private readonly IIdentityUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public UserService(IIdentityUnitOfWork identityUnitOfWork, IMapper mapper)
        {
            unitOfWork = identityUnitOfWork;
            this.mapper = mapper;
        }

        public async Task<SignInResult> SignIn(SignInModel model)
        {
            var result = await unitOfWork.SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

            return result;
        }

        public async Task<IdentityResult> SignUp(SignUpModel model)
        {
            var user = mapper.Map<User>(model);

            var result = await unitOfWork.UserManager.CreateAsync(user, model.Password);

            return result;
        }

        public Task SignOut()
        {
            return unitOfWork.SignInManager.SignOutAsync();
        }

        #region Disposable

        private bool disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    unitOfWork.Dispose();
                }

                disposed = true;
            }
        }

        ~UserService()
        {
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            System.GC.SuppressFinalize(this);
        }

        #endregion Disposable
    }
}