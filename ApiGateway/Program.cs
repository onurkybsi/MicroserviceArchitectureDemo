using System.IO;
using KybInfrastructure.Host;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace ApiGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configuration = InitialHelper.GetConfiguration(Directory.GetCurrentDirectory(), System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));

            Log.Logger = InitialHelper.CreateELKLogger(new ELKLoggerSettings
            {
                AppName = configuration["ASPNETCORE_APPLICATIONNAME"],
                ElasticsearchURL = configuration["ELASTICSEARCH_URL"]
            });

            var host = CreateHostBuilder(args, configuration).Build();

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args, IConfiguration configuration)
            => Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(x => x.AddConfiguration(configuration))
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseKestrel();
                    webBuilder.UseStartup<Startup>();
                }).ConfigureLogging(config => config.ClearProviders()).UseSerilog();
    }
}
