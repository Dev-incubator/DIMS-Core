using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DIMS_Core.Common.Extensions;
using DIMS_Core.Mailer.Services;
using NUnit.Framework;

namespace DIMS_Core.Tests
{
    public class SenderTest
    {
        private readonly Sender _sender;

        public SenderTest()
        {
            _sender = new Sender(null);
        }

        [Test]
        [TestCase("Test message",
            "<div><h3>This is test message from DIMS Core</h3></div>",
            null)] // TODO: here need to write your email for testing.
        public async Task SendMessageAsync(string subject, string body, string email)
        {
            if (subject.IsNullOrWhiteSpace() 
                || email.IsNullOrWhiteSpace() 
                || email.IsNullOrWhiteSpace())
            {
                Assert.Fail("You didn't set email/body/subject.");

                return;
            }

            var result = await _sender.SendMessageAsync(subject, body, email);

            Assert.IsTrue(result);
        }
    }
}
