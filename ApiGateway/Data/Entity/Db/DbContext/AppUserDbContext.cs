using Microsoft.EntityFrameworkCore;

namespace ApiGateway.Data.AppUser
{
    public class AppUserDbContext : DbContext
    {
        public AppUserDbContext() { }

        public AppUserDbContext(DbContextOptions<AppUserDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=AppUserDb;Trusted_Connection=true");
        }

        public DbSet<AppUser> AppUsers { get; set; }
    }
}