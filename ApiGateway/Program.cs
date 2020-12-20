using System;
using System.Collections.Generic;
using System.IO;
using ApiGateway.Data;
using ApiGateway.Data.AppUser;
using ApiGateway.Services.Auth;
using Infrastructure.Utility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Elasticsearch;
using Serilog.Sinks.Elasticsearch;

namespace ApiGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configuration = InitialHelper.GetConfiguration(Directory.GetCurrentDirectory(), Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));

            Log.Logger = CreateSerilogLogger(configuration);

            var host = CreateHostBuilder(args, configuration).Build();

            SeedAppUserDb(host, GetAdminUser(configuration));

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args, IConfiguration configuration) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(x => x.AddConfiguration(configuration))
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseKestrel();
                    webBuilder.UseStartup<Startup>();
                }).ConfigureLogging(config => config.ClearProviders()).UseSerilog();

        private static Serilog.ILogger CreateSerilogLogger(IConfiguration configuration)
        {
            return new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithProperty("Application", "ApiGateway")
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .WriteTo.Console()
                .WriteTo.Elasticsearch(
                    new ElasticsearchSinkOptions(
                        new Uri("http://localhost:9200/"))
                    {
                        CustomFormatter = new ExceptionAsObjectJsonFormatter(renderMessage: true),
                        AutoRegisterTemplate = true,
                        TemplateName = "serilog-events-template",
                        IndexFormat = "apigateway-log-{0:yyyy.MM.dd}"
                    })
                .MinimumLevel.Verbose()
                .CreateLogger();
        }

        private static void SeedAppUserDb(IHost host, AppUser admin)
        {
            try
            {
                InitialHelper.SeedData<ApiGatewayDbContext, AppUser>(new List<AppUser> { admin });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred seeding the AppUserDb.");
            }

        }

        private static AppUser GetAdminUser(IConfiguration configuration)
            => new AppUser
            {
                Email = configuration["APP_ADMIN_USER"],
                HashedPassword = EncryptionHelper.CreateHashed(configuration["APP_ADMIN_PASSWORD"])
            };
    }
}
