using Microsoft.EntityFrameworkCore;
using TourPlanner.Models;

namespace TourPlanner.DAL
{
    public class PsqlTestContext : DbContext
    {
        readonly string connectionString;

        public DbSet<PersonTestModel> People { get; set; }

        public PsqlTestContext(string connectionString)
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
