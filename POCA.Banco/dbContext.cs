using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace POCA.Banco
{
    public class dbContext : DbContext
    {
        public dbContext(DbContextOptions<dbContext> options) : base(options) { }

        // Defina suas DbSets aqui

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("appsettings.json")
                    .Build();

                var connectionString = "Server=localhost;Port=3306;Database=bd_trabalho;User=root;Password=root;" /*configuration.GetConnectionString("DefaultConnection")*/;
                optionsBuilder.UseMySql(connectionString,ServerVersion.AutoDetect(connectionString));
            }
        }
    }
}
