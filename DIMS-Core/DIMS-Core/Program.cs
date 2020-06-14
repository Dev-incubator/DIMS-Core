using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using DIMS_Core.Logger.Extensions;

namespace DIMS_Core
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseCustomNLog();
        }
    }
}