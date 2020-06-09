using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using NLog.Web;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.IO;
using System;

namespace Library.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory() + "/Configs")
                .AddJsonFile("UISetting.json");

            var config = builder.Build();
            foreach (var item in config.AsEnumerable())
            {
                Console.WriteLine($"Key: {item.Key}, Value: {item.Value}");
            }
            Console.WriteLine("FontFamily:" + config["FontFamily"]);
            Console.WriteLine("FontSize:" + config.GetValue<int>("FontSize"));
            Console.WriteLine("Editor Foreground: " + config["Editor:Foreground"]);

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((builderContext, config) =>
                {
                    var env = builderContext.HostingEnvironment;
                    Console.WriteLine("ConfigureAppConfiguration env: " + env);
                    Console.WriteLine("ConfigureAppConfiguration env.ContentRootPath: " + env.ContentRootPath);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureLogging(logging =>
                {
                    logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                })
                .UseNLog();  // NLog: Setup NLog for Dependency injection
    }
}
