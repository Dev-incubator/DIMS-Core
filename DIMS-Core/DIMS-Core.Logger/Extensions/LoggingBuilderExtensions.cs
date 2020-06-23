using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using System;

namespace DIMS_Core.Logger.Extensions
{
    public static class LoggingBuilderExtensions
    {
        public static void ConfigureNlogByJson(this ILoggingBuilder logging, string configFileName)
        {
            logging.ClearProviders(); //clear default asp.net provider

            var configBuilder = new ConfigurationBuilder()
                .AddJsonFile(configFileName)
                .Build();

            var config = configBuilder?.GetSection("Nlog");

            if (config is null || !config.Exists())
            {
                throw new Exception("Section NLog wasn't found.");
            }

            logging.AddNLog(new NLogLoggingConfiguration(config));
        }
    }
}