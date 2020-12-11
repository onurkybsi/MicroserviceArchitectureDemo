using System;
using System.Linq;
using Grpc.Core;
using Infrastructure.Framework.Grpc;
using Infrastructure.Model;
using Infrastructure.Utility;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.ObjectPool;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Host
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddJwtAuth(this IServiceCollection services, IAuthConfig authConfig)
        {
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = authConfig.Issuer,
                        ValidAudience = authConfig.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(authConfig.SecurityKey),
                        ClockSkew = TimeSpan.Zero
                    };
                });

            return services;
        }

        public static IServiceCollection RegisterModule(this IServiceCollection services, IModuleDescriptor moduleDescriptor)
        {
            moduleDescriptor.GetDescriptions().ForEach(description => services.Add(description));

            return services;
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

        public static IServiceCollection AddGrpcClientCustomPool<TClient>(this IServiceCollection services, GrpcClientPoolConfig clientConfig)
            where TClient : ClientBase
        {
            services.AddSingleton(p =>
            {
                return new GrpcClientPool<TClient>(clientConfig);
            });

            services.Add(ServiceDescriptor.Describe(typeof(TClient), (ServiceProvider) =>
            {
                var pool = ServiceProvider.GetRequiredService<GrpcClientPool<TClient>>();

                return pool.Get();
            }, ServiceLifetime.Transient));

            return services;
        }

        public static IServiceCollection AddGrpcClientPool<TClient>(this IServiceCollection services, GrpcClientPoolConfig clientConfig)
            where TClient : ClientBase
        {
            if (!services.Any(x => x.ServiceType == typeof(ObjectPoolProvider)))
                services.TryAddSingleton<ObjectPoolProvider, DefaultObjectPoolProvider>();

            services.TryAddSingleton<ObjectPool<TClient>>(serviceProvider =>
            {
                var provider = serviceProvider.GetRequiredService<ObjectPoolProvider>();

                var channel = new Channel(clientConfig.TargetServerURL, ChannelCredentials.Insecure);

                var policy = new GrpcClientPooledObjectPolicy<TClient>(channel);

                return provider.Create(policy);
            });

            services.AddTransient<TClient>(sp => sp.GetRequiredService<ObjectPool<TClient>>().Get());

            return services;
        }
    }
}