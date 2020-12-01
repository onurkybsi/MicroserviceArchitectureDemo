using System.Text;
using ApiGateway.Model;
using Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using static ApiGateway.Services.GrpcService.Helper;

namespace ApiGateway
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        // Sadece bu yetmiyor mu ? Neden yukardakide var ?
        public static IConfiguration StaticConfiguration { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            StaticConfiguration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            Log.Information("ApiGateway listening...");

            services.ConfigureManuelly<AuthConfig>(Configuration, ac => ac.SecurityKey = Encoding.ASCII.GetBytes(Configuration["AuthConfig:SecurityKey"]));

            services.AddSingleton<IAuthConfig>(sp => sp.GetRequiredService<AuthConfig>());

            services.AddJwtAuth(new AuthConfig
            {
                SecurityKey = Encoding.ASCII.GetBytes(Configuration["AuthConfig:SecurityKey"]),
                Audience = Configuration["AuthConfig:Audience"],
                Issuer = Configuration["AuthConfig:Issuer"]
            });

            RegisterModules(services);

            RegisterGrpcChannels(services);

            services.AddControllers()
                .AddNewtonsoftJson();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            Log.Information($"ApiGateway on: {env.EnvironmentName}");

            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else if (env.IsProduction())
                app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(c => c
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void RegisterModules(IServiceCollection services)
        {
            services.RegisterModule(Data.Descriptor.GetDescriptor());
            services.RegisterModule(Services.Descriptor.GetDescriptor());
        }

        private void RegisterGrpcChannels(IServiceCollection services)
        {
            // More client objects can reuse the same channel. 
            // Creating a channel is an expensive operation compared to invoking a remote call 
            // so in general you should reuse a single channel for as many calls as possible.

            // Yeni servisler eklenince else if ile yeni servisleri ekle !
            // Bu yapı hızlı mı emin miyiz ? Test edelim diğer yollar ile karşılaştıralım.
            services.AddSingleton<ChannelResolver>(cr => (target) =>
            {
                if (target == Configuration["PRODUCT_SERVICE_URL"])
                    return new Grpc.Core.Channel(Configuration["PRODUCT_SERVICE_URL"], Grpc.Core.ChannelCredentials.Insecure);

                return null;
            });
        }
    }
}
