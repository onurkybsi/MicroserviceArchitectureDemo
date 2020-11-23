using System;
using System.Text;
using ApiGateway.Data.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace ApiGateway.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        private static bool isEnvDev = Startup.StaticConfiguration["ASPNETCORE_ENVIRONMENT"] == "Development";

        public static IServiceCollection AddJwtAuth(this IServiceCollection services, AuthConfig jWTAuthConfig)
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
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jWTAuthConfig.SecurityKey)),
                        ClockSkew = TimeSpan.Zero
                    };
                });

            return services;
        }
    }
}