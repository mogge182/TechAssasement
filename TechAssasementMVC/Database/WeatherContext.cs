using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;

namespace TechAssasementMVC.Database
{
    public class WeatherContext : DbContext
    {
        public WeatherContext(DbContextOptions<WeatherContext> options) : base(options) { }

        public DbSet<WeatherData> WeatherData { get; set; }
        public DbSet<Location> Locations { get; set; }

    }
}
