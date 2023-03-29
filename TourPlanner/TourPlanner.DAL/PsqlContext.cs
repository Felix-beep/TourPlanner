using Microsoft.EntityFrameworkCore;
using TourPlanner.Models;

namespace TourPlanner.DAL
{
    public class PsqlContext : DbContext
    {
        readonly string connectionString;

        public DbSet<PersonTestModel> People { get; set; }

        public PsqlContext(string connectionString)
        {
            this.connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(connectionString:connectionString);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
