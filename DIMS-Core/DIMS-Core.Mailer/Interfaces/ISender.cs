using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DIMS_Core.Mailer.Interfaces
{
    public interface ISender
    {
        Task<bool> SendMessageAsync(string email, string subject, string html);

        Task SendMessageAsync(IEnumerable<string> emails, string subject, string html);
    }
}