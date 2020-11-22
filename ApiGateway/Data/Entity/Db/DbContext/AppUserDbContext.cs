using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ApiGateway.Data.AppUser
{
    public class AppUserDbContext : DbContext
    {
        public AppUserDbContext() { }

        public AppUserDbContext(DbContextOptions<AppUserDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=app-user-mssql;Database=AppUserDb;User=sa;Password=App_user_mssql_pass123!");
        }

        public DbSet<AppUser> AppUsers { get; set; }
    }
}