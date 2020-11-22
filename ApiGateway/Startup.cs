using ApiGateway.Data.AppUser;
using ApiGateway.Data.Entity;
using ApiGateway.Infrastructure;
using ApiGateway.Services.Auth;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ApiGateway
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            InitializeDb();

            services.AddDbContext<AppUserDbContext>(options => options.UseSqlServer(Configuration["AppUserDbConnection"]));

            services.AddScoped<IAppUserRepo, AppUserRepo>();

            SetConfigurues(services);

            services.AddJwtAuth(new JWTAuthConfig
            {
                Issuer = Configuration["JWTAuthConfig:Issuer"],
                Audience = Configuration["JWTAuthConfig:Audience"],
                SecurityKey = Configuration["JWTAuthConfig:SecurityKey"]
            });

            services.AddScoped<IAuthService, AuthService>();

            services.AddControllers();
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
        }

        private static void InitializeDb()
        {
            using (var context = new AppUserDbContext())
            {
                if (context.Database.EnsureCreated())
                {
                    var admin = new AppUser
                    {
                        Email = "onurbpm@outlook.com",
                        HashedPassword = EncryptionHelper.CreateHash("testparola123"),
                        Role = Role.User
                    };

                    context.AppUsers.Add(admin);

                    context.SaveChanges();
                }
            }
        }

        private void SetConfigurues(IServiceCollection services)
        {
            services.Configure<JWTAuthConfig>(Configuration.GetSection("JWTAuthConfig"));
        }
    }
}
