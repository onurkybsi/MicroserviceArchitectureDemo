using KybInfrastructure.Framework.Grpc;
using KybInfrastructure.Host;
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

            app.UseSerilogRequestLogging();

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

        private void RegisterGrpcClients(IServiceCollection services)
        {
            // TO-DO: Tüm optionlar yük testi yapılıp denenmeli !

            // Option - 1 
            // Test - 10: 30 / Test - 100: 28 / Test - 1000: 29ms
            // TO-DO: Burda ki lifetime cycle ı değiştirip deneyelim
            // services.AddSingleton(sp =>
            // {
            //     // TO-DO: ChannelCredentials ayarlanmalı
            //     var channel = new Grpc.Core.Channel(Configuration["PRODUCT_SERVICE_URL"], Grpc.Core.ChannelCredentials.Insecure);

            //     return new Service.ProductService.ProductServiceClient(channel);
            // });

            // Option - 2 ObjectPool pattern using custom pool
            // Test - 10: 30 / Test - 100: 28 / Test - 1000: 29ms
            // services.AddGrpcClientCustomPool<Service.ProductService.ProductServiceClient>(new GrpcClientPoolConfig { TargetServerURL = Configuration["PRODUCT_SERVICE_URL"] });

            // Option - 3 ObjectPool pattern using .NET Core pool
            // Test - 10: 31 / Test - 100: 30.5 / Test - 1000: 29ms
            services.AddGrpcClientPool<Service.ProductService.ProductServiceClient>(new GrpcClientPoolSettings { TargetServerURL = Configuration["PRODUCT_SERVICE_URL"] });
        }
    }
}
