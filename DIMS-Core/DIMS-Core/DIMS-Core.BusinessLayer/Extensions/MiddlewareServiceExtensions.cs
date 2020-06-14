using Microsoft.Extensions.DependencyInjection;

namespace DIMS_Core.BusinessLayer.Extensions
{
    public static class MiddlewareServiceExtensions
    {
        public static IServiceCollection AddDependenceInjections(this IServiceCollection services)
        {
            return services;
        }
    }
}