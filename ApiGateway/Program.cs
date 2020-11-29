using System;
using System.Collections.Generic;
using System.IO;
using ApiGateway.Data;
using ApiGateway.Data.AppUser;
using ApiGateway.Services.Auth;
using Infrastructure;
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
            => new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

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
