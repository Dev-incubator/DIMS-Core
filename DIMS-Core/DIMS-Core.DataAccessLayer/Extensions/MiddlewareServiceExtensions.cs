using DIMS_Core.DataAccessLayer.Context;
using DIMS_Core.DataAccessLayer.Interfaces;
using DIMS_Core.DataAccessLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DIMS_Core.DataAccessLayer.Extensions
{
    public static class MiddlewareServiceExtensions
    {
        public static IServiceCollection AddDatabaseDependencies(this IServiceCollection services)
        {
            services.AddTransient<ISampleRepository, SampleRepository>();
            services.AddTransient<IDirectionRepository, DirectionRepository>();
            services.AddTransient<ITaskRepository, TaskRepository>();
            services.AddTransient<ITaskStateRepository, TaskStateRepository>();
            services.AddTransient<ITaskTrackRepository, TaskTrackRepository>();
            services.AddTransient<IUserProfileRepository, UserProfileRepository>();
            services.AddTransient<IUserTaskRepository, UserTaskRepository>();
            services.AddTransient<IVTaskRepository, VTaskRepository>();
            services.AddTransient<IVUserProfileRepository, VUserProfileRepository>();
            services.AddTransient<IVUserProgressRepository, VUserProgressRepository>();
            services.AddTransient<IVUserTaskRepository, VUserTaskRepository>();
            services.AddTransient<IVUserTrackRepository, VUserTrackRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            return services;
        }

        public static IServiceCollection AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DIMSCoreDataBaseContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DIMSDatabase"));
            });

            return services;
        }
    }
}