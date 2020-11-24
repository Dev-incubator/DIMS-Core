using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DIMS_Core.BusinessLayer.Interfaces;
using DIMS_Core.BusinessLayer.Models.Account;
using DIMS_Core.Identity.Entities;
using DIMS_Core.Identity.Services;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using DIMS_Core.BusinessLayer.Models.Direction;

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

        public IEnumerable<Role> GetRoles()
        {
            return unitOfWork.RoleManager.Roles;
        }

        public User GetUser(SignUpModel signUpModel)
        {
            return unitOfWork.UserManager.Users.FirstOrDefault(x => x.Email == signUpModel.Email);
        }

        public async Task UpdateRole(User user, string role)
        {
            if (user is null || role is null)
            {
                return;
            }


            var userRoles = await unitOfWork.UserManager.GetRolesAsync(user);
            if (!userRoles.Contains(role))
            {
                if (userRoles.Count > 0) userRoles.Clear();
                await unitOfWork.UserManager.AddToRoleAsync(user, role);
            }
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