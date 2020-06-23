using DIMS_Core.Identity.Configs;
using DIMS_Core.Identity.Entities;
using DIMS_Core.Identity.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DIMS_Core.Identity.Extensions
{
    public static class MiddlewareServiceExtensions
    {
        public static IServiceCollection AddIdentityContext(this IServiceCollection services)
        {
            var config = new IdentityConfiguration();

            services.AddDbContext<IdentityContext>(options =>
            {
                options.UseSqlServer(config.ConnectionString);
            });

            services.AddIdentity<User, Role>(configs =>
                {
                    configs.User.RequireUniqueEmail = true;
                    configs.Password = new PasswordOptions
                    {
                        RequireDigit = false,
                        RequireLowercase = false,
                        RequireNonAlphanumeric = false,
                        RequireUppercase = false,
                        RequiredLength = 5
                    };
                })
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders();

            return services;
        }

        public static IServiceCollection AddIndentityDependencies(this IServiceCollection services)
        {
            services.AddTransient<IIdentityUnitOfWork, IdentityUnitOfWork>();

            return services;
        }
    }
}