using DIMS_Core.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using System;

namespace DIMS_Core.Identity.Services
{
    public class IdentityUnitOfWork : IIdentityUnitOfWork
    {
        public UserManager<User> UserManager { get; }
        public SignInManager<User> SignInManager { get; }

        public IdentityUnitOfWork(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
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