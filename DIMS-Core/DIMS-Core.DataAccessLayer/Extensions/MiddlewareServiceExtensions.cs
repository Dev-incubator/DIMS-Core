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
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<DbContext, DIMSCoreContext>();

            return services;
        }

        public static IServiceCollection AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DIMSCoreContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DIMSDatabase"));
            });

            return services;
        }
    }
}