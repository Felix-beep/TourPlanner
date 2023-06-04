using TourPlanner.BL;
using TourPlanner.DAL;
using TourPlanner.Models;

namespace TourPlanner.Tests
{
    public class APITest
    {
        ConnectionModeFactory repofactory;
        APIRouteClient routeClient;

        [OneTimeSetUp]
        public void Setup()
        {
            log4net.Config.BasicConfigurator.Configure();

            var apiUri = new Uri("https://dev2.gasstationsoftware.net/");

            var repo = new APITourRepository();
            repo.Connect(apiUri);
            repofactory = new ConnectionModeFactory(repo, null);

            repofactory.SwapMode();
            
            routeClient = new APIRouteClient(apiUri);
        }

        [Test]
        public async Task ApiGetRouteImageTest()
        {
            var filePath = "api_map_image.jpg";

            var req = routeClient.GetBuilder()
                .SetRequestType(IRequestBuilder.RequestType.MapImage)
                .SetLocationFrom("Denver%2C+CO")
                .SetLocationTo("Boulder%2C+CO");

            await routeClient.RequestImageAsync(req, filePath);
            
            Assert.IsTrue(File.Exists(filePath));
        }

        [Test]
        public async Task ApiGetCachedImageTest()
        {
            var filePath = "api_cached_map_imgage.jpg";

            var route = await routeClient.RequestTourData("Denver%2C+CO", "Boulder%2C+CO", TransportType.fastest);

            Guid imageID = Guid.Parse(route.imageID);

            var req = routeClient.GetBuilder()
                .SetRequestType(IRequestBuilder.RequestType.MapImage)
                .SetImageID(imageID);
            await routeClient.RequestImageAsync(req, filePath);

            Assert.IsTrue(File.Exists(filePath));
        }

        [TestCase("Denver%2C+CO", "Boulder%2C+CO", true)]
        [TestCase("nonexistent loc", "nonexistent loc", false)]
        public async Task ApiGetTourData(string from, string to, bool success)
        {
            var newTour = await routeClient.RequestTourData(from, to, TransportType.fastest);

            if (success)
            {
                Assert.That(newTour, Is.Not.Null);
            }
            else
            {
                Assert.That(newTour, Is.Null);
            }
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
        public async Task ApiIntegrationTest() 
        {
            Console.WriteLine("Getting tours:");
            await PrintTours();

            Console.WriteLine("\nGetting tours after insert:");
            await repofactory.GetRepo().InsertTourAsync(SampleExtensions.CreateSampleTour(100, new List<TourLog>()));
            await PrintTours();

            Assert.That((await repofactory.GetRepo().GetToursAsync()).Count(), Is.EqualTo(8 + 1));  

            Console.WriteLine("\nGetting tours after update:");
            var tour3 = await repofactory.GetRepo().GetTourAsync(3);
            tour3.description = "UPDATED DESCRIPTION";
            tour3.name = "UPDATED NAME";
            await repofactory.GetRepo().UpdateTourAsync(tour3);
            await PrintTours();

            Assert.That((await repofactory.GetRepo().GetTourAsync(3)).name, Is.EqualTo("UPDATED NAME"));

            Console.WriteLine("\nGetting tours after delete:");
            await repofactory.GetRepo().DeleteTourAsync(5);
            await PrintTours();

            Assert.That((await repofactory.GetRepo().GetToursAsync()).Count(), Is.EqualTo(8));

            Console.WriteLine("\nGetting tours after tour log update:");
            var tour6 = await repofactory.GetRepo().GetTourAsync(6);
            var tour6log = tour6.logs.First();
            tour6log.comment = "UPDATED TOUR LOG";
            await repofactory.GetRepo().UpdateTourLogAsync(tour6log);
            await PrintTours();

            Assert.That((await repofactory.GetRepo().GetTourAsync(6)).logs.First().comment, Is.EqualTo("UPDATED TOUR LOG"));
        }
    }
}
