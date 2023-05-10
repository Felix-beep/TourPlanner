using TourPlanner.BL;
using TourPlanner.DAL;
using TourPlanner.Models;

namespace TourPlanner.Tests
{
    public class BLTest
    {
        [OneTimeSetUp]
        public void Setup()
        {
            log4net.Config.BasicConfigurator.Configure();
        }

        [Test]
        public void ExportTest() 
        {
            var repo = new MemoryTourRepository();
            repo.LoadSampleData();

            var bl = new BackgroundLogic(repo);
            bl.ExportTours(bl.GetAllTours());
        }

        [Test]
        public void ImportTest()
        {
            var repo = new MemoryTourRepository();

            var bl = new BackgroundLogic(repo);
            bl.ImportTours(new string[] { "Subject2.csv" });
        }
    }
}
