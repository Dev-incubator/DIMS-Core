using AutoMapper;
using DIMS_Core.BusinessLayer.Interfaces;
using DIMS_Core.BusinessLayer.Models.Account;
using DIMS_Core.Identity.Entities;
using DIMS_Core.Identity.Services;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace DIMS_Core.BusinessLayer.Services
{
    internal class UserIdentityService : IUserIdentityService
    {
        private readonly IIdentityUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public UserIdentityService(IIdentityUnitOfWork identityUnitOfWork, IMapper mapper)
        {
            unitOfWork = identityUnitOfWork;
            this.mapper = mapper;
        }

        public async Task<SignInResult> SignInAsync(SignInModel model)
        {
            var result = await unitOfWork.SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

            return result;
        }

        public async Task<IdentityResult> SignUpAsync(SignUpModel model)
        {
            var mappedEntity = mapper.Map<User>(model);

            var result = await unitOfWork.UserManager.CreateAsync(mappedEntity, model.Password);
            if (result.Succeeded)
            {
                mapper.Map(mappedEntity, model);
                await unitOfWork.UserManager.AddToRoleAsync(mappedEntity, model.UserRole.ToString());
            }

            return result;
        }

        public async Task<IdentityResult> DeleteAsync(int Id)
        {
            var user = await unitOfWork.UserManager.FindByIdAsync(Id.ToString());
            var result = await unitOfWork.UserManager.DeleteAsync(user);

            return result;
        }

        public async Task<IdentityResult> DeleteAsync(string Email)
        {
            var user = await unitOfWork.UserManager.FindByEmailAsync(Email);
            var result = await unitOfWork.UserManager.DeleteAsync(user);

            return result;
        }

        public Task SignOutAsync()
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

        ~UserIdentityService()
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