using TourPlanner.DAL;
using TourPlanner.Models;

namespace TourPlanner.Tests
{
    public class APITest
    {
        APITourRepository repo;

        [OneTimeSetUp]
        public void Setup()
        {
            log4net.Config.BasicConfigurator.Configure();
            repo = new APITourRepository();
            repo.Connect(new Uri("http://localhost:5000/"));
        }

        async Task PrintTours()
        {
            foreach (var t in await repo.GetToursAsync())
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
            await repo.InsertTourAsync(SampleExtensions.CreateSampleTour(100, new List<TourLog>()));
            await PrintTours();

            Console.WriteLine("\nGetting tours after update:");
            await repo.UpdateTourAsync(new Tour
            {
                ID = 3,
                name = "UPDATED TOUR WITH ID 3",
                description = "UPDATED DESCRIPTION OF TOUR 3",
            });
            await PrintTours();

            Console.WriteLine("\nGetting tours after delete:");
            await repo.DeleteTourAsync(5);
            await PrintTours();
        }
    }
}
