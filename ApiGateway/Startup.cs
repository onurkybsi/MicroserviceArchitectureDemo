using System.Text;
using ApiGateway.Data.AppUser;
using ApiGateway.Infrastructure;
using ApiGateway.Model;
using ApiGateway.Services.Auth;
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

            // ConfigureAuthConfig(services);
            services.ConfigureManuel<AuthConfig>(Configuration, ac => ac.SecurityKey = Encoding.ASCII.GetBytes(Configuration["AuthConfig:SecurityKey"]));

            services.AddSingleton<IAuthConfig>(sp => sp.GetRequiredService<AuthConfig>());

            services.RegisterModule(new Data.Descriptor());
            services.RegisterModule(new Services.Descriptor());

            services.AddControllers()
                .AddNewtonsoftJson();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            Log.Information($"ApiGateway on: {env.EnvironmentName}, Connection string: {Configuration["APPUSERDB_CONNECTION_STRING"]}");

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
        private void ConfigureAuthConfig(IServiceCollection services)
        {
            var authConfig = new AuthConfig();

            Configuration.GetSection("AuthConfig").Bind(authConfig);

            authConfig.SecurityKey = Encoding.ASCII.GetBytes(Configuration["AuthConfig:SecurityKey"]);

            services.Add(ServiceDescriptor.Singleton(authConfig));
        }
    }
}
