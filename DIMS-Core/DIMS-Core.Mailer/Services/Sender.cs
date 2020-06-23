using DIMS_Core.Mailer.Configs;
using DIMS_Core.Mailer.Interfaces;
using Microsoft.Extensions.Logging;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DIMS_Core.Mailer.Services
{
    internal class Sender : ISender
    {
        private const string _layoutHtml =
            "<div style=\"margin-top: 20px;\">Best regards, Dev Incubator Inc.</div>" +
            "<div><img src=\"https://i.ibb.co/9tSLsd6/logo-name.png\" style=\"margin-top:26px; width:250px !important; height:100px !important;\"/>" +
            "</div>";

        private readonly MailerCofiguration cofiguration;
        private readonly ILogger logger;

        public Sender(ILogger logger)
        {
            cofiguration = new MailerCofiguration();
            this.logger = logger;
        }

        public async Task<bool> SendMessageAsync(string email, string subject, string body)
        {
            RestClient client = new RestClient
            {
                BaseUrl = new Uri(cofiguration.ApiUrl),
                Authenticator = new HttpBasicAuthenticator("api", cofiguration.ApiUrl)
            };

            var request = GetPostRequest(email, subject, body);
            var response = await client.ExecuteAsync(request);

            logger?.LogInformation("Message was sent to {$email}. Status code: {$status}, error message: {$error}.",
                email,
                response.StatusCode,
                response.ErrorMessage);

            return response.IsSuccessful;
        }

        public async Task SendMessageAsync(IEnumerable<string> emails, string subject, string body)
        {
            foreach (var email in emails)
            {
                await SendMessageAsync(email, subject, body);
            }
        }

        private RestRequest GetPostRequest(string email, string subject, string body)
        {
            var htmlContent = "<div>" + body + _layoutHtml + "</div>";

            RestRequest request = new RestRequest();
            request.AddParameter("subject", subject);
            request.AddParameter("domain", cofiguration.Domain, ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", cofiguration.FromAddress);
            request.AddParameter("to", email);
            request.AddParameter("html", htmlContent);
            request.Method = Method.POST;

            return request;
        }
    }
}