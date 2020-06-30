using DIMS_Core.Common.Services;
using Microsoft.Extensions.Configuration;

namespace DIMS_Core.Mailer.Configs
{
    internal class MailerCofiguration : BaseCustomConfiguration
    {
        private const string fileName = "mailersettings.json";

        private SmtpSettings _smtpSettings;

        public SmtpSettings SmtpSettings => _smtpSettings ??= Configuration.GetSection(SmtpSettings.ConfigKey)
                                                                           .Get<SmtpSettings>();

        protected override IConfiguration BuildConfiguration()
        {
            var builder = new ConfigurationBuilder();

            return builder
                .AddJsonFile(fileName)
                .Build();
        }
    }
}