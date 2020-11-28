using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace ApiGateway.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        private static bool isEnvDev = Startup.StaticConfiguration["ASPNETCORE_ENVIRONMENT"] == "Development";

        public static IServiceCollection AddJwtAuth(this IServiceCollection services, IAuthConfig jWTAuthConfig)
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
                        ValidateAudience = !isEnvDev,
                        ValidateIssuer = !isEnvDev,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jWTAuthConfig.Issuer,
                        ValidAudience = jWTAuthConfig.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(new byte[1]),
                        ClockSkew = TimeSpan.Zero
                    };
                });

            return services;
        }

        public static IServiceCollection RegisterModule(this IServiceCollection services, IModuleDescriptor moduleDescriptor)
        {
            moduleDescriptor.Describe().ForEach(description => services.Add(description));

            return services;
        }

        public static IServiceCollection ConfigureManuel<TConfig>(this IServiceCollection services, IConfiguration Configuration, Action<TConfig> manuelSettings)
            where TConfig : class, new()
        {
            var mapped = (TConfig)Activator.CreateInstance(typeof(TConfig));

            Configuration.GetSection(typeof(TConfig).Name).Bind(mapped);

            manuelSettings(mapped);

            services.Add(ServiceDescriptor.Singleton(mapped));

            return services;
        }
    }
}