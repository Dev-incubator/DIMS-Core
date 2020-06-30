using DIMS_Core.Common.Extensions;
using DIMS_Core.Mailer.Configs;
using DIMS_Core.Mailer.Interfaces;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using MimeKit;
using System.Linq;
using System.Threading.Tasks;

namespace DIMS_Core.Mailer.Services
{
    public class Sender : ISender
    {
        private const string _layoutHtml =
            "<div style=\"margin-top: 20px;\">Best regards, Dev Incubator Inc.</div>" +
            "<div><img src=\"https://i.ibb.co/9tSLsd6/logo-name.png\" style=\"margin-top:26px; width:250px !important; height:100px !important;\"/>" +
            "</div>";

        private readonly SmtpSettings smtpSettings;
        private readonly ILogger logger;

        public Sender(ILogger logger)
        {
            var config = new MailerCofiguration();

            smtpSettings = config.SmtpSettings;
            this.logger = logger;
        }

        public async Task<bool> SendMessageAsync(string subject,
            string body,
            params string[] emails)
        {
            using var smtpClient = new SmtpClient();
            var isValid = false;

            smtpClient.MessageSent += (sender, e) =>
            {
                logger?.LogInformation("Message was sent to {$email}. Response: {$response}.", emails.ToSeparatedString("; "), e.Response);

                isValid = true;
            };

            await smtpClient.ConnectAsync(smtpSettings.Server, smtpSettings.Port, smtpSettings.EnableSsl);

            await smtpClient.AuthenticateAsync(smtpSettings.UserName, smtpSettings.Password);

            var mailMessage = GenerateMessage(subject, body, emails);
            await smtpClient.SendAsync(mailMessage);

            await smtpClient.DisconnectAsync(true);

            return isValid;
        }

        private MimeMessage GenerateMessage(string subject,
            string body,
            params string[] to)
        {
            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = body + _layoutHtml
            };

            var message = new MimeMessage
            {
                Body = bodyBuilder.ToMessageBody(),
                Subject = subject
            };

            message.From.Add(new MailboxAddress(smtpSettings.UserName, smtpSettings.UserName));
            message.To.AddRange(to.Select(q => new MailboxAddress(q, q)));

            return message;
        }
    }
}