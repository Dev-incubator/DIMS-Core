using DIMS_Core.Common.Services;
using Microsoft.Extensions.Configuration;

namespace DIMS_Core.Identity.Configs
{
    public class IdentityConfiguration : BaseCustomConfiguration
    {
        private const string fileName = "identitysettings.json";

        public string ConnectionString => GetSection("ConnectionString");

        protected override IConfiguration BuildConfiguration()
        {
            var builder = new ConfigurationBuilder();

            return builder.AddJsonFile(fileName)
                .Build();
        }
    }
}