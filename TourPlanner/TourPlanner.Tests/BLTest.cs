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
        public async Task ExportTest() 
        {
            var repo = new MemoryTourRepository();
            repo.LoadSampleDataAsync();

            var bl = new BackgroundLogic(repo);
            await bl.ExportToursAsync(await bl.GetAllToursAsync());
        }

        [Test]
        public async Task ImportTest()
        {
            var repo = new MemoryTourRepository();

            var bl = new BackgroundLogic(repo);
            await bl.ImportToursAsync(new string[] { "Subject2.csv" });
        }
    }
}
