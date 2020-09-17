using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Org.BouncyCastle.Asn1.X509.Qualified;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using System;

namespace TestProject.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.File(@"C:\temp\log.txt", rollingInterval: RollingInterval.Day, outputTemplate: "{Timestamp:o} [{Level:u4}]: {Message:lj}{NewLine}{Exception}")
                .WriteTo.Console(theme: AnsiConsoleTheme.Literate, outputTemplate: "{Timestamp:o} [{Level:u4}] <{SourceContext}>: {Message:lj}{NewLine}{Exception}")
                .CreateLogger();

            try
            {
                CreateHostBuilder(args).Build().Run();
                return;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "There was a problem starting the service: {ex}", ex);
                return;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseSerilog();
    }
}
