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

            routeClient = new APIRouteClient(apiUri);
            
        }

        [Test]
        public async Task ApiGetRouteImageTest()
        {
            var req = routeClient.GetBuilder(null);
            req.SetRequestType(IRequestBuilder.RequestType.MapImage);
            req.SetLocationFrom("Denver%2C+CO");
            req.SetLocationTo("Boulder%2C+CO");
            await routeClient.RequestImageAsync(req, "api_map_image.jpg");
        }

        [Test]
        public async Task ApiGetCachedImageTest()
        {
            var req = routeClient.GetBuilder(null);
            req.SetRequestType(IRequestBuilder.RequestType.MapImage);
            req.SetImageID(Guid.Parse("00000000-0000-0000-0000-000000000000"));
            await routeClient.RequestImageAsync(req, "api_cached_map_imgage.jpg");
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
