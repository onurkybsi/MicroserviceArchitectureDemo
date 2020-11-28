using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace ApiGateway.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        private static bool isEnvDev = Startup.StaticConfiguration["ASPNETCORE_ENVIRONMENT"] == "Development";

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
                        ValidateAudience = !isEnvDev,
                        ValidateIssuer = !isEnvDev,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = authConfig.Issuer,
                        ValidAudience = authConfig.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(new byte[1]),
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
    }
}