using AutoMapper;
using DIMS_Core.BusinessLayer.Interfaces;
using DIMS_Core.BusinessLayer.Services;
using DIMS_Core.DataAccessLayer.Extensions;
using DIMS_Core.Identity.Extensions;
using DIMS_Core.Mailer.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DIMS_Core.BusinessLayer.Extensions
{
    public static class MiddlewareServiceExtensions
    {
        public static IServiceCollection AddDependencyInjections(this IServiceCollection services)
        {
            services.AddTransient<ISampleService, SampleService>();
            services.AddTransient<IUserService, UserService>();

            services.AddDatabaseDependencies()
                .AddIndentityDependencies()
                .AddMailerDependencies();

            return services;
        }

        public static IServiceCollection AddAutomapperProfiles(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }

        public static IServiceCollection AddCustomSolutionConfigs(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDependencyInjections()
                .AddDatabaseContext(configuration)
                .AddAutomapperProfiles()
                .AddIdentityContext();

            return services;
        }
    }
}