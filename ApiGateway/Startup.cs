using System.Text;
using ApiGateway.Model;
using Infrastructure.Framework.Grpc;
using Infrastructure.Host;
using Infrastructure.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
            ConfigureAuth(services);

            RegisterModules(services);

            RegisterGrpcClients(services);

            services.AddControllers()
                .AddNewtonsoftJson();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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

            Log.ForContext<Startup>().Information("{Application} is listening on {Env}...", env.ApplicationName, env.EnvironmentName);
        }

        private void ConfigureAuth(IServiceCollection services)
        {
            services.ConfigureManuelly<AuthConfig>(Configuration, ac => ac.SecurityKey = Encoding.ASCII.GetBytes(Configuration["AuthConfig:SecurityKey"]));

            services.AddSingleton<IAuthConfig>(sp => sp.GetRequiredService<AuthConfig>());

            services.AddJwtAuth(new AuthConfig
            {
                SecurityKey = Encoding.ASCII.GetBytes(Configuration["AuthConfig:SecurityKey"]),
                Audience = Configuration["AuthConfig:Audience"],
                Issuer = Configuration["AuthConfig:Issuer"]
            });
        }
        private static void RegisterModules(IServiceCollection services)
        {
            services.RegisterModule(Data.Descriptor.GetDescriptor());
            services.RegisterModule(Services.Descriptor.GetDescriptor());
        }
        private void RegisterGrpcClients(IServiceCollection services)
        {
            // TO-DO: Tüm optionlar yük testi yapılıp denenmeli !

            // Option - 1 
            // TO-DO: Burda ki lifetime cycle ı değiştirip deneyelim
            // services.AddSingleton(sp =>
            // {
            //     // TO-DO: ChannelCredentials ayarlanmalı
            //     var channel = new Channel(Configuration["PRODUCT_SERVICE_URL"], ChannelCredentials.Insecure);

            //     return new Service.ProductService.ProductServiceClient(channel);
            // });

            // Option - 2 ObjectPool pattern using custom pool
            // services.AddGrpcClientCustomPool<Service.ProductService.ProductServiceClient>(new GrpcClientPoolConfig { TargetServerURL = Configuration["PRODUCT_SERVICE_URL"] });

            // Option - 3 ObjectPool pattern using .NET Core pool
            services.AddGrpcClientPool<Service.ProductService.ProductServiceClient>(new GrpcClientPoolConfig { TargetServerURL = Configuration["PRODUCT_SERVICE_URL"] });
        }
    }
}
