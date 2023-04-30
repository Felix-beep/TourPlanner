using TourPlanner.DAL;

namespace TourPlanner.Tests
{
    public class APITest
    {
        APITourRepository repo;

        [OneTimeSetUp]
        public void Setup()
        {
            repo = new APITourRepository();
            repo.Connect(new Uri("https://dev2.gasstationsoftware.net/"));
        }

        void PrintTours()
        {
            foreach (var t in repo.GetTours())
            {
                Console.WriteLine(t);
                foreach (var tl in t.logs)
                    Console.WriteLine($"\t{tl}");
            }
        }

        [Test]
        public void BasicTest() 
        {
            Console.WriteLine("Getting tours:");
            PrintTours();

            Console.WriteLine("\nGetting tours after insert:");
            var newTour = new Models.Tour
            {
                name = "new test tour",
                description = "Description",
            };
            repo.InsertTour(newTour);
            PrintTours();

            Console.WriteLine("\nGetting tours after update:");
            repo.UpdateTour(new Models.Tour
            {
                ID = 3,
                name = "updated tour with id 3",
                description = "Description",
            });
            PrintTours();

            Console.WriteLine("\nGetting tours after delete:");
            repo.DeleteTour(5);
            PrintTours();
        }
    }
}
