using System.Text;
using ApiGateway.Model;
using Infrastructure;
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
    }
}
