using TourPlanner.BL;

namespace TourPlanner.Tests
{
    public class APITest
    {
        APITourRepository repo;

        [OneTimeSetUp]
        public void Setup()
        {
            repo = new APITourRepository();
            repo.Connect(new Uri("http://localhost:5000/"));
        }

        [Test]
        public void BasicTest() 
        {
            Console.WriteLine("Getting tours:");
            foreach (var t in repo.GetTours()) 
                Console.WriteLine(t);

            Console.WriteLine("\nGetting tours after insert:");
            repo.InsertTour(new Models.Tour
            {
                name = "new test tour",
                description = "Description",
            });
            foreach (var t in repo.GetTours())
                Console.WriteLine(t);

            Console.WriteLine("\nGetting tours after update:");
            repo.UpdateTour(new Models.Tour
            {
                ID = 3,
                name = "updated tour with id 3",
                description = "Description",
            });
            foreach (var t in repo.GetTours())
                Console.WriteLine(t);

            Console.WriteLine("\nGetting tours after delete:");
            repo.DeleteTour(5);
            foreach (var t in repo.GetTours())
                Console.WriteLine(t);

        }
    }
}
