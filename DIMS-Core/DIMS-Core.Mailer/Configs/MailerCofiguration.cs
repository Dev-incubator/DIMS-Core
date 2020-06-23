using DIMS_Core.Common.Services;
using Microsoft.Extensions.Configuration;

namespace DIMS_Core.Mailer.Configs
{
    internal class MailerCofiguration : BaseCustomConfiguration
    {
        private const string fileName = "mailersettings.json";

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