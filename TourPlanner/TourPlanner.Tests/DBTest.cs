using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using TourPlanner.DAL;

namespace TourPlanner.Tests
{
    public class DBTest
    {
        DbContext db;

        [OneTimeSetUp]
        public void Setup()
        {
            var connection = new SqliteConnection("Filename=:memory:");

        }

        [Test]
        public void Test() 
        { 
            



        }
    }
}
