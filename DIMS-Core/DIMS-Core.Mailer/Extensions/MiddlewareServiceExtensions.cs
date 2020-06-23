using DIMS_Core.Mailer.Interfaces;
using DIMS_Core.Mailer.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DIMS_Core.Mailer.Extensions
{
    public static class MiddlewareServiceExtensions
    {
        public static IServiceCollection AddMailer(this IServiceCollection services)
        {
            services.AddTransient<ISender, Sender>();

            return services;
        }
    }
}