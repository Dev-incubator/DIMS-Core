using DIMS_Core.Identity.Configs;
using DIMS_Core.Identity.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DIMS_Core.Identity.Services
{
    public class IdentityContext : IdentityDbContext<User, Role, int>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
        {
        }

        protected IdentityContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configs = new IdentityConfiguration();

            optionsBuilder.UseSqlServer(configs.ConnectionString);
        }
    }
}