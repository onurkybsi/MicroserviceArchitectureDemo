using System;
using System.Collections.Generic;
using System.IO;
using ApiGateway.Data.Entity.AppUser;
using ApiGateway.Infrastructure;
using ApiGateway.Services.Auth;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace ApiGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configuration = GetConfiguration();

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

        private static IConfiguration GetConfiguration()
            => new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

        private static Serilog.ILogger CreateSerilogLogger(IConfiguration configuration)
            => new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

        private static void SeedAppUserDb(IHost host, AppUser admin)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    Initializer.SeedData<AppUserDbContext, AppUser>(services, new List<AppUser> { admin });
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "An error occurred seeding the AppUserDb.");
                }
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
