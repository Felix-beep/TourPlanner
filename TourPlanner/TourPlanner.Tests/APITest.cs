using TourPlanner.DAL;
using TourPlanner.Models;

namespace TourPlanner.Tests
{
    public class APITest
    {
        ITourRepository repo;

        [OneTimeSetUp]
        public void Setup()
        {
            log4net.Config.BasicConfigurator.Configure();
            var repo = new APITourRepository();
            repo.Connect(new Uri("http://localhost:5000/"));
            
            this.repo = repo;
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
            var tour3 = await repo.GetTourAsync(3);
            tour3.description = "UPDATED DESCRIPTION";
            tour3.name = "UPDATED NAME";
            await repo.UpdateTourAsync(tour3);
            await PrintTours();

            Console.WriteLine("\nGetting tours after delete:");
            await repo.DeleteTourAsync(5);
            await PrintTours();

            Console.WriteLine("\nGetting tours after tour log update:");
            var tour6 = await repo.GetTourAsync(6);
            var tour6log = tour6.logs.First();
            tour6log.comment = "UPDATED TOUR LOG";
            await repo.UpdateTourLogAsync(tour6log);
            await PrintTours();
        }
    }
}
