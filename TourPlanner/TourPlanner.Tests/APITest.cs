using TourPlanner.BL;
using TourPlanner.DAL;
using TourPlanner.Models;

namespace TourPlanner.Tests
{
    public class APITest
    {
        ConnectionModeFactory repofactory;

        [OneTimeSetUp]
        public void Setup()
        {
            log4net.Config.BasicConfigurator.Configure();

            var repo = new APITourRepository();
            repofactory = new ConnectionModeFactory(repo, null);
            
        }

        async Task PrintTours()
        {
            foreach (var t in await repofactory.GetRepo().GetToursAsync())
            {
                Console.WriteLine(t.CustomToString());
                foreach (var tl in t.logs)
                    Console.WriteLine($"\t{tl}");
            }
        }

        [Test]
        public async Task BasicTest() 
        {
            Console.WriteLine("Getting tours:");
            await PrintTours();

            Console.WriteLine("\nGetting tours after insert:");
            await repofactory.GetRepo().InsertTourAsync(SampleExtensions.CreateSampleTour(100, new List<TourLog>()));
            await PrintTours();

            Console.WriteLine("\nGetting tours after update:");
            var tour3 = await repofactory.GetRepo().GetTourAsync(3);
            tour3.description = "UPDATED DESCRIPTION";
            tour3.name = "UPDATED NAME";
            await repofactory.GetRepo().UpdateTourAsync(tour3);
            await PrintTours();

            Console.WriteLine("\nGetting tours after delete:");
            await repofactory.GetRepo().DeleteTourAsync(5);
            await PrintTours();

            Console.WriteLine("\nGetting tours after tour log update:");
            var tour6 = await repofactory.GetRepo().GetTourAsync(6);
            var tour6log = tour6.logs.First();
            tour6log.comment = "UPDATED TOUR LOG";
            await repofactory.GetRepo().UpdateTourLogAsync(tour6log);
            await PrintTours();
        }
    }
}
