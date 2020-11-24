using DIMS_Core.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace DIMS_Core.Identity.Services
{
    internal class IdentityUnitOfWork : IIdentityUnitOfWork
    {
        public UserManager<User> UserManager { get; }
        public RoleManager<Role> RoleManager { get; }
        public SignInManager<User> SignInManager { get; }

        public IdentityUnitOfWork(
            UserManager<User> userManager, 
            SignInManager<User> signInManager,
            RoleManager<Role> roleManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            RoleManager = roleManager;
        }

        #region Disposable

        private bool disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    UserManager.Dispose();
                }

                disposed = true;
            }
        }

        ~IdentityUnitOfWork()
        {
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion Disposable
    }
}