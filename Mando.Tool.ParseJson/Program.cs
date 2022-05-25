using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Mando.Tool.ParseJson
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            try
            {
                Log.Information("Starting ParseJson..");

                var builder = new HostBuilder()
                    .ConfigureAppConfiguration((hostContext, configAppBulder) =>
                    {
                        configAppBulder.AddJsonFile("appsettings.json", optional: false);
                        configAppBulder.AddEnvironmentVariables();
                        configAppBulder.AddCommandLine(args);
                    })
                    .ConfigureServices((hostContext, services) =>
                    {
                        services.AddTransient<IApplication, Application>();
                    })
                    .UseSerilog();

                builder.Build().Services.GetService<IApplication>().Run();

            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "ParseJson terminated unexpectantly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

    }
}