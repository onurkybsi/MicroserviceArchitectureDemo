using System.Collections.Generic;
using System.Text;
using ApiGateway.Model;
using Grpc.Core;
using Infrastructure;
using Infrastructure.Grpc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.ObjectPool;
using Serilog;

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
            services.ConfigureManuelly<AuthConfig>(Configuration, ac => ac.SecurityKey = Encoding.ASCII.GetBytes(Configuration["AuthConfig:SecurityKey"]));

            services.AddSingleton<IAuthConfig>(sp => sp.GetRequiredService<AuthConfig>());

            services.AddJwtAuth(new AuthConfig
            {
                SecurityKey = Encoding.ASCII.GetBytes(Configuration["AuthConfig:SecurityKey"]),
                Audience = Configuration["AuthConfig:Audience"],
                Issuer = Configuration["AuthConfig:Issuer"]
            });

            RegisterModules(services);

            RegisterGrpcClients(services);

            services.AddControllers()
                .AddNewtonsoftJson();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            Log.Information($"ApiGateway listening on: {env.EnvironmentName}");

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

        private void RegisterGrpcClients(IServiceCollection services)
        {
            // Option - 1
            // TO-DO: Yük testi yapımalı ! Duruma göre Scoped, Transient olarak değiştirilebilir.
            // services.AddSingleton(sp =>
            // {
            //     // TO-DO: ChannelCredentials ayarlanmalı
            //     var channel = new Channel(Configuration["PRODUCT_SERVICE_URL"], ChannelCredentials.Insecure);

            //     return new Service.ProductService.ProductServiceClient(channel);
            // });


            // Option - 2 ObjectPool pattern
            services.AddGrpcClientPool<Service.ProductService.ProductServiceClient>(new GrpcClientPoolConfig { TargetServerURL = Configuration["PRODUCT_SERVICE_URL"] });
        }
    }
}
