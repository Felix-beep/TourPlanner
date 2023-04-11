using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TourPlanner.Models;

namespace TourPlanner.DAL
{
    public class PsqlContext : DbContext
    {
        readonly string contextString;
        readonly IConfiguration config;

        public DbSet<Tour> Tours { get; set; }
        public DbSet<TourLog> TourLogs { get; set; }

        public PsqlContext(IConfiguration configuration, string contextString)
        {
            config = configuration;
            this.contextString = contextString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(config.GetConnectionString(contextString));
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasPostgresExtension("postgis");
        }
    }
}
