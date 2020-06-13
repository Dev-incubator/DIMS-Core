using Microsoft.AspNetCore.Hosting;
using NLog.Web;

namespace DIMS_Core.Logger.Extensions
{
    public static class WebHostBuilderExtensions
    {
        private const string defaultConfigFileName = "nlogconfig.json";

        public static IWebHostBuilder UseCustomNLog(this IWebHostBuilder builder, string configFileName = defaultConfigFileName)
        {
            return builder.ConfigureLogging(logging =>
            {
                logging.ConfigureNlogByJson(configFileName);
            })
            .UseNLog();
        }
    }
}