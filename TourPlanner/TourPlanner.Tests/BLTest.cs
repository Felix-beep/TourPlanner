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
            // These samples get exported:
            //var samples = new Tour[]
            //{
            //    new Tour(1, "Hello Subject1", "description1", new List<TourLog>()),
            //    new Tour(2, "Subject2", "description2", new List<TourLog>()),
            //    new Tour(3, "Subject3", "description3", new List<TourLog>()),
            //    new Tour(4, "Food", "Food", new List<TourLog>()),
            //};

            var repo = new ConnectionModeFactory(null, new MemoryTourRepository());

            var bl = new BackendLogic(repo);
            await bl.ExportToursAsync(await bl.GetAllToursAsync());

            Assert.That(File.Exists("Hello Subject1.csv"));
            Assert.That(File.Exists("Subject2.csv"));
            Assert.That(File.Exists("Subject3.csv"));
            Assert.That(File.Exists("Food.csv"));
        }

        [Test]
        public async Task ImportTest()
        {
            var repo = new ConnectionModeFactory(null, new MemoryTourRepository());

            var bl = new BackendLogic(repo);
            await bl.ImportToursAsync(new string[] { "Subject2.csv" });

            Assert.That((await repo.GetRepo().GetToursAsync()).Count(), Is.EqualTo(4 + 1));
        }
    }
}
