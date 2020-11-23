using ApiGateway.Data.Entity.AppUser;
using ApiGateway.Data.Model;
using ApiGateway.Infrastructure;
using ApiGateway.Services.Auth;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace ApiGateway
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public static IConfiguration StaticConfiguration { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            StaticConfiguration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            Log.Information("ApiGateway listening...");

            services.AddDbContext<AppUserDbContext>(options => options.UseSqlServer(Configuration["APPUSERDB_CONNECTION_STRING"]));

            services.AddScoped<IAppUserRepo, AppUserRepo>();

            MapConfigurations(services);

            services.AddJwtAuth(new AuthConfig
            {
                Issuer = Configuration["AuthConfig:Issuer"],
                Audience = Configuration["AuthConfig:Audience"],
                SecurityKey = Configuration["AuthConfig:SecurityKey"]
            });

            services.AddScoped<IAuthService, AuthService>();

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

        private void MapConfigurations(IServiceCollection services)
        {
            services.Configure<AuthConfig>(Configuration.GetSection("AuthConfig"));
        }
    }
}
