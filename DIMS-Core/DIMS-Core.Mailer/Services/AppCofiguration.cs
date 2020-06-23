using DIMS_Core.Common.Services;
using Microsoft.Extensions.Configuration;
using System;

namespace DIMS_Core.Mailer.Services
{
    internal class AppCofiguration : BaseCustomConfiguration
    {
        private const string fileName = "appsettings.json";

        public string Domain => GetSection("Domain");
        public string AuthenticationToken => GetSection("AuthenticationToken");
        public string ApiUrl => GetSection("ApiUrl");
        public string FromAddress => GetSection("FromAddress");

        protected override IConfiguration BuildConfiguration()
        {
            var builder = new ConfigurationBuilder();

            return builder
                .AddJsonFile(fileName)
                .Build();
        }
    }
}