using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace DIMS_Core.Identity.Services
{
    public class IdentityContext : DbContext
    {
        public IdentityContext([NotNull] DbContextOptions options) : base(options)
        {
        }

        protected IdentityContext()
        {
        }
    }
}