using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CaveServer
{
    public class Program
    {
        public const string ENV_VARS_PREFIX = "ASPNETCORE_";

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.AddEnvironmentVariables(ENV_VARS_PREFIX);
            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                var port = Environment.GetEnvironmentVariable("PORT");
                if (!string.IsNullOrEmpty(port) && int.TryParse(port, out int portInt))
                    webBuilder.UseUrls($"http://*:{port}");
                webBuilder.UseStartup<Startup>();
            });
    }
}
