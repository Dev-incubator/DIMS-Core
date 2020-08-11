using AutoMapper;
using DIMS_Core.BusinessLayer.Interfaces;
using DIMS_Core.BusinessLayer.Services;
using DIMS_Core.DataAccessLayer.Extensions;
using DIMS_Core.Identity.Extensions;
using DIMS_Core.Mailer.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Reflection;

namespace DIMS_Core.BusinessLayer.Extensions
{
    public static class MiddlewareServiceExtensions
    {
        public static IServiceCollection AddDependencyInjections(this IServiceCollection services)
        {
            services.AddTransient<ISampleService, SampleService>();
            services.AddTransient<IUserIdentityService, UserIdentityService>();
            services.AddTransient<IDirectionService, DirectionService>();
            services.AddTransient<ITaskService, TaskService>();
            services.AddTransient<ITaskStateService, TaskStateService>();
            services.AddTransient<ITaskTrackService, TaskTrackService>();
            services.AddTransient<IUserProfileService, UserProfileService>();
            services.AddTransient<IUserTaskService, UserTaskService>();
            services.AddTransient<IVTaskService, VTaskService>();
            services.AddTransient<IVUserProfileService, VUserProfileService>();
            services.AddTransient<IVUserProgressService, VUserProgressService>();
            services.AddTransient<IVUserTaskService, VUserTaskService>();
            services.AddTransient<IVUserTrackService, VUserTrackService>();
            services.AddTransient<ITaskManager, TaskManager>();
            services.AddTransient<IUserService, UserService>();

            services.AddDatabaseDependencies()
                .AddIndentityDependencies()
                .AddMailerDependencies();

            return services;
        }

        public static IServiceCollection AddAutomapperProfiles(this IServiceCollection services, params Assembly[] otherMapperAssemblies)
        {
            List<Assembly> assemblies = new List<Assembly>(otherMapperAssemblies)
            {
                Assembly.GetExecutingAssembly()
            };
            services.AddAutoMapper(assemblies);

            return services;
        }

        public static IServiceCollection AddCustomSolutionConfigs(this IServiceCollection services, IConfiguration configuration, params Assembly[] otherMapperAssemblies)
        {
            services.AddDependencyInjections()
                .AddDatabaseContext(configuration)
                .AddAutomapperProfiles(otherMapperAssemblies)
                .AddIdentityContext();

            return services;
        }
    }
}