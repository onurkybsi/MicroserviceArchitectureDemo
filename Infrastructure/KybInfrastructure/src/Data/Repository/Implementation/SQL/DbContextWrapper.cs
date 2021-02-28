using Microsoft.EntityFrameworkCore;

namespace KybInfrastructure.Data
{
    public class DbContextWrapper : DbContext
    {
        protected IDatabaseSettings _databaseSettings;

        public DbContextWrapper(IDatabaseSettings databaseSettings)
        {
            _databaseSettings = databaseSettings;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(_databaseSettings.ConnectionString);
        }
    }
}