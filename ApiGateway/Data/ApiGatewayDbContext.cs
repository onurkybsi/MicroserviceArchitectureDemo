using System;
using ApiGateway.Model;
using Microsoft.EntityFrameworkCore;

namespace ApiGateway.Data
{
    public class ApiGatewayDbContext : DbContext
    {
        public ApiGatewayDbContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Startup.StaticConfiguration["APPUSERDB_CONNECTION_STRING"]);
        }

        public DbSet<ApiGateway.Data.AppUser.AppUser> AppUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApiGateway.Data.AppUser.AppUser>()
                .HasIndex(c => c.Id)
                .IsUnique();

            builder.Entity<ApiGateway.Data.AppUser.AppUser>()
                .HasAlternateKey(c => c.Email);

            builder.Entity<ApiGateway.Data.AppUser.AppUser>()
                .Property(c => c.HashedPassword)
                .IsRequired();

            builder.Entity<ApiGateway.Data.AppUser.AppUser>()
                .Property(c => c.HashedPassword)
                .IsRequired();

            builder.Entity<ApiGateway.Data.AppUser.AppUser>()
                .Property(c => c.Role)
                .HasDefaultValue(Role.User);

            builder.Entity<ApiGateway.Data.AppUser.AppUser>()
                .Property(c => c.SystemEntryDate)
                .HasDefaultValue(DateTime.Now);
        }
    }
}