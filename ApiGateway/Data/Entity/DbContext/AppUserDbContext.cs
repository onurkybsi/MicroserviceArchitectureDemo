using System;
using ApiGateway.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace ApiGateway.Data.Entity.AppUser
{
    public class AppUserDbContext : DbContext
    {
        public AppUserDbContext(DbContextOptions<AppUserDbContext> options) : base(options) { }

        public DbSet<AppUser> AppUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AppUser>()
                .HasIndex(c => c.Id)
                .IsUnique();

            builder.Entity<AppUser>()
                .HasAlternateKey(c => c.Email);

            builder.Entity<AppUser>()
                .Property(c => c.HashedPassword)
                .IsRequired();

            builder.Entity<AppUser>()
                .Property(c => c.HashedPassword)
                .IsRequired();

            builder.Entity<AppUser>()
                .Property(c => c.Role)
                .HasDefaultValue(Role.User);

            builder.Entity<AppUser>()
                .Property(c => c.SystemEntryDate)
                .HasDefaultValue(DateTime.Now);
        }
    }
}