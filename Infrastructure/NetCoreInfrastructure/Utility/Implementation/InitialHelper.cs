using System;
using System.Collections.Generic;
using Infrastructre.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Elasticsearch;
using Serilog.Sinks.Elasticsearch;

namespace Infrastructure.Utility
{
    public static class InitialHelper
    {
        public static void SeedData<TContext, TEntity>(IServiceProvider serviceProvider, List<TEntity> entities)
            where TContext : DbContext
            where TEntity : class
        {
            using (var context = (TContext)Activator.CreateInstance(typeof(TContext),
                new object[] { serviceProvider.GetRequiredService<DbContextOptions<TContext>>() }))
            {
                if (context.Database.EnsureCreated())
                {
                    var dbSet = context.Set<TEntity>();

                    dbSet.AddRange(entities);

                    context.SaveChanges();
                }
            }
        }

        public static void SeedData<TContext, TEntity>(List<TEntity> entities)
            where TContext : DbContext
            where TEntity : class
        {
            using (var context = (TContext)Activator.CreateInstance(typeof(TContext)))
            {
                if (context.Database.EnsureCreated())
                {
                    var dbSet = context.Set<TEntity>();

                    dbSet.AddRange(entities);

                    context.SaveChanges();
                }
            }
        }

        public static IConfiguration GetConfiguration(string basePath, string environment)
            => new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

        public static Serilog.ILogger CreateELKLogger(ELKLoggerConfig config)
        {
            if (config is null || string.IsNullOrEmpty(config.AppName) || string.IsNullOrEmpty(config.ElasticsearchURL))
                throw new ArgumentNullException(nameof(ELKLoggerConfig) + "must be configured");

            return new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithProperty("Application", config.AppName)
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .WriteTo.Console()
                .WriteTo.Elasticsearch(
                    new ElasticsearchSinkOptions(
                        new Uri(config.ElasticsearchURL))
                    {
                        CustomFormatter = new ExceptionAsObjectJsonFormatter(renderMessage: true),
                        AutoRegisterTemplate = true,
                        TemplateName = "serilog-events-template",
                        IndexFormat = string.Format("{0}-logs", config.AppName.ToLower())
                    })
                .MinimumLevel.Verbose()
                .CreateLogger();
        }
    }
}