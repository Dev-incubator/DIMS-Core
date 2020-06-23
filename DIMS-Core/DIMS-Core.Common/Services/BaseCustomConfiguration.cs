using DIMS_Core.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using System;

namespace DIMS_Core.Common.Services
{
    public abstract class BaseCustomConfiguration : ICustomConfiguration
    {
        public IConfiguration Configuration { get; }

        public BaseCustomConfiguration()
        {
            Configuration = BuildConfiguration();
        }

        public string GetSection(string name) => Configuration?.GetSection(name)?.Value ?? throw new Exception($"Incorrect configuration file. Section name: '{name}'");

        protected abstract IConfiguration BuildConfiguration();
    }
}