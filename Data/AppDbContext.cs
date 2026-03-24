using Microsoft.EntityFrameworkCore;
using STCP_WeatherForecast.Models;

namespace STCP_WeatherForecast.Data
{
    // Handles database connection using ORM
    public class AppDbContext : DbContext
    {
        public DbSet<Weather> WeatherData { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
    }
}