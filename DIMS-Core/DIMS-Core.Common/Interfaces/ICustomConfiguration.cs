using Microsoft.Extensions.Configuration;

namespace DIMS_Core.Common.Interfaces
{
    public interface ICustomConfiguration
    {
        IConfiguration Configuration { get; }

        string GetSection(string name);
    }
}