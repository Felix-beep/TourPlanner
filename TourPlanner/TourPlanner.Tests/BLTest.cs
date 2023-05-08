using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using TourPlanner.BL;
using TourPlanner.DAL;

namespace TourPlanner.Tests
{
    public class BLTest
    {
        BackgroundLogic BL;

        [OneTimeSetUp]
        public void Setup()
        {
            BL = new BackgroundLogic(new APITourRepository());

        }

        [Test]
        public void Test() 
        { 

        }
    }
}
