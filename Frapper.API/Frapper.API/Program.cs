using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NLog.Web;

namespace Frapper.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //documentation-https://tutexchange.com/learn-asp-net-core-web-api-from-project/
            //postman collection-https://camo.githubusercontent.com/16a903fe0c8e857e22585b47d674a11dc7fd16a2d4ef6a2d0e932e70a62cb0d6/68747470733a2f2f72756e2e7073746d6e2e696f2f627574746f6e2e737667
            //db script- https://github.com/saineshwar/Frapper.API/blob/main/Database_Script/FrapperAPIDBscript.sql

            var logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

            try
            {
                CreateHostBuilder(args).Build().Run();

            }
            catch (Exception exception)
            {
                logger.Error(exception, "Stopped program because of exception");
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                NLog.LogManager.Shutdown();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Error);
                })
                .UseNLog();  // NLog: Setup NLog for Dependency injection
    }
}
