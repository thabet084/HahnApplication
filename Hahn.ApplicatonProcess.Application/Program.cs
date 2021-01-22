using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Formatting.Compact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.Application
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //using var loggerFactory = LoggerFactory.Create(builder =>
            //{
            //    builder
            //        .AddFilter("Microsoft", LogLevel.Warning)
            //        .AddFilter("System", LogLevel.Warning)
            //        .AddConsole()
            //        .AddEventLog();
            //});
            //ILogger logger = loggerFactory.CreateLogger<Program>();
            //logger.LogInformation("Example log message");

            var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

            Log.Logger = new LoggerConfiguration()
                   .MinimumLevel.Debug()
                 .ReadFrom.Configuration(configuration)
                .CreateLogger();


            try
            {
                Log.Information("app starting");
                CreateHostBuilder(args).Build().Run();
                Log.Information("app terminated");
            }
            catch (Exception e)
            {
                Log.Fatal("app crashed", e.Message);
            }

            Log.CloseAndFlush();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
       Host.CreateDefaultBuilder(args)
           .UseServiceProviderFactory(new AutofacServiceProviderFactory())
           .ConfigureWebHostDefaults(webBuilder =>
           {
               webBuilder.UseStartup<Startup>();
           })
           .UseSerilog();
    }
}

