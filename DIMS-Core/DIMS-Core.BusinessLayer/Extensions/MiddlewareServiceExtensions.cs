using AutoMapper;
using DIMS_Core.BusinessLayer.Interfaces;
using DIMS_Core.BusinessLayer.Services;
using DIMS_Core.DataAccessLayer.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DIMS_Core.BusinessLayer.Extensions
{
    public static class MiddlewareServiceExtensions
    {
        public static IServiceCollection AddDependencyInjections(this IServiceCollection services)
        {
            services.AddTransient<ISampleService, SampleService>();

            services.AddDatabaseDependencies();

            return services;
        }

        public static IServiceCollection AddAutomapperProfiles(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}