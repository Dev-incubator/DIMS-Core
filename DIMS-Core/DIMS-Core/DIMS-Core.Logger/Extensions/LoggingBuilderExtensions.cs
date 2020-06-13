using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using System;
using System.IO;

namespace DIMS_Core.Logger.Extensions
{
    public static class LoggingBuilderExtensions
    {
        public static void ConfigureNlogByJson(this ILoggingBuilder logging, string configFileName)
        {
            logging.ClearProviders(); //clear default asp.net provider

            if (!File.Exists(Path.Combine(Directory.GetCurrentDirectory(), configFileName)))
            {
                throw new Exception("Config file wasn't found.");
            }

            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
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