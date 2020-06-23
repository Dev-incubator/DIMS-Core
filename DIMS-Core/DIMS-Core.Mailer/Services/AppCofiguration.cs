using Microsoft.Extensions.Configuration;
using System;

namespace DIMS_Core.Mailer.Services
{
    internal class AppCofiguration
    {
        private const string fileName = "appsettings.json";

        private readonly IConfiguration configuration;

        public string Domain => GetSection("Domain");
        public string AuthenticationToken => GetSection("AuthenticationToken");
        public string ApiUrl => GetSection("ApiUrl");
        public string FromAddress => GetSection("FromAddress");

        public AppCofiguration()
        {
            configuration = new ConfigurationBuilder()
                .AddJsonFile(fileName)
                .Build();
        }

        private string GetSection(string name) => configuration.GetSection(name)?.Value ?? throw new Exception($"Section {name} cannot be empty.");
    }
}