using Microsoft.EntityFrameworkCore;

namespace ColtonsApp.DatabaseContexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }

        public DbSet<WeatherForecast> WeatherForecasts { get; set; }
    }
}
