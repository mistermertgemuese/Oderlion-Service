using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.IO;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace Oderlion_Service
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            BuildConfig(builder);

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Build())
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File(@"log\log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            Log.Logger.Information("Application Starting");

            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddTransient<IGreetingService, GreetingService>();
                    services.AddTransient<IFTPService, FTPService>();
                    services.AddTransient<IMailService, MailService>();
                })
                .UseSerilog()
                .Build();

            var svc1 = ActivatorUtilities.CreateInstance<GreetingService>(host.Services);
            var svc_ftp = ActivatorUtilities.CreateInstance<FTPService>(host.Services);
            var svc_mail = ActivatorUtilities.CreateInstance<MailService>(host.Services);

            svc1.Run();
            svc_ftp.Run();
            svc_mail.Run();
        }

        static void BuildConfig(IConfigurationBuilder builder)
        {
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
                .AddEnvironmentVariables();
        }
    }
}