using System;
using System.Text;
using KybInfrastructure.Utility;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.ObjectPool;
using Grpc.Core;
using System.Linq;
using KybInfrastructure.Framework.Grpc;

namespace KybInfrastructure.Host
{
    public static class Extensions
    {
        public static IServiceCollection RegisterModule(this IServiceCollection services, IModuleDescriptor moduleDescriptor)
        {
            moduleDescriptor.GetDescriptions().ForEach(description => services.Add(description));

            return services;
        }

        public static IServiceCollection AddBasicJwtAuthentication(this IServiceCollection services, JwtAuthenticationContext context)
        {
            bool envIsDevelopment = context.Environment != Environment.Development;

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = envIsDevelopment;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = envIsDevelopment,
                        ValidateIssuer = envIsDevelopment,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = context.Issuer,
                        ValidAudience = context.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(context.SecurityKey)),
                        ClockSkew = TimeSpan.Zero
                    };
                });

            return services;
        }

        public static string GetAspNetCoreEnvironmentName(this IConfiguration configuration)
            => configuration["ASPNETCORE_ENVIRONMENT"];

        public static Environment GetAspNetCoreEnvironment(this IConfiguration configuration)
        {
            Environment environment = Environment.Development;

            switch (configuration["ASPNETCORE_ENVIRONMENT"])
            {
                case "Staging":
                    environment = Environment.Staging;
                    break;
                case "Production":
                    environment = Environment.Production;
                    break;
                default:
                    environment = Environment.Development;
                    break;
            }

            return environment;
        }

        public static IServiceCollection ConfigureManuelly<TConfig>(this IServiceCollection services, IConfiguration Configuration, Action<TConfig> manuelSettings)
            where TConfig : class, new()
        {
            var mapped = (TConfig)Activator.CreateInstance(typeof(TConfig));

            Configuration.GetSection(typeof(TConfig).Name).Bind(mapped);

            manuelSettings(mapped);

            services.Add(ServiceDescriptor.Singleton(mapped));

            return services;
        }

        public static IServiceCollection AddGrpcClientCustomPool<TClient>(this IServiceCollection services, GrpcClientPoolSettings clientSettings)
            where TClient : ClientBase
        {
            services.AddSingleton(p =>
            {
                return new GrpcClientPool<TClient>(clientSettings);
            });

            services.Add(ServiceDescriptor.Describe(typeof(TClient), (ServiceProvider) =>
            {
                var pool = ServiceProvider.GetRequiredService<GrpcClientPool<TClient>>();

                return pool.Get();
            }, ServiceLifetime.Transient));

            return services;
        }

        public static IServiceCollection AddGrpcClientPool<TClient>(this IServiceCollection services, GrpcClientPoolSettings clientSettings)
            where TClient : ClientBase
        {
            if (!services.Any(x => x.ServiceType == typeof(ObjectPoolProvider)))
                services.TryAddSingleton<ObjectPoolProvider, DefaultObjectPoolProvider>();

            services.TryAddSingleton<ObjectPool<TClient>>(serviceProvider =>
            {
                var provider = serviceProvider.GetRequiredService<ObjectPoolProvider>();

                var channel = new Channel(clientSettings.TargetServerURL, ChannelCredentials.Insecure);

                var policy = new GrpcClientPooledObjectPolicy<TClient>(channel);

                return provider.Create(policy);
            });

            services.AddTransient<TClient>(sp => sp.GetRequiredService<ObjectPool<TClient>>().Get());

            return services;
        }
    }
}