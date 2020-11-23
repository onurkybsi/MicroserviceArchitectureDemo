using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ApiGateway.Infrastructure
{
    public static class Initializer
    {
        public static void SeedData<TContext, TEntity>(IServiceProvider serviceProvider, List<TEntity> entities)
            where TContext : DbContext
            where TEntity : class
        {
            using (var context = (TContext)Activator.CreateInstance(typeof(TContext),
                new object[] { serviceProvider.GetRequiredService<DbContextOptions<TContext>>() }))
            {
                if (context.Database.EnsureCreated())
                {
                    var dbSet = context.Set<TEntity>();

                    dbSet.AddRange(entities);

                    context.SaveChanges();
                }
            }
        }

        public static void SeedData<TContext, TEntity>(List<TEntity> entities)
            where TContext : DbContext
            where TEntity : class
        {
            using (var context = (TContext)Activator.CreateInstance(typeof(TContext)))
            {
                if (context.Database.EnsureCreated())
                {
                    var dbSet = context.Set<TEntity>();

                    dbSet.AddRange(entities);

                    context.SaveChanges();
                }
            }
        }
    }
}