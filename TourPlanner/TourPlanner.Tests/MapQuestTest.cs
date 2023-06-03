using Microsoft.Extensions.Configuration;
using TourPlanner.BL;
using TourPlanner.DAL;

namespace TourPlanner.Tests
{
    public class MapQuestTest
    {
        string apiKey;

        [OneTimeSetUp]
        public void LoadConfig()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            apiKey = config.GetSection("ApiKeys")["MapQuestKey"];
        }

        [Test]
        public async Task TestRequestRoute()
        {
            var client = new MapQuestClient();

            var req = client.GetBuilder(apiKey)
                .SetRequestType(IRequestBuilder.RequestType.Route)
                .SetLocationFrom("Denver%2C+CO")
                .SetLocationTo("Boulder%2C+CO");

            Assert.That(
                await client.RequestJsonStringAsync(req), 
                Contains.Substring("\"hasTunnel\":false"));
        }

        [Test]
        public async Task TestRequestImage()
        {
            var client = new MapQuestClient();

            var req = client.GetBuilder(apiKey)
                .SetRequestType(IRequestBuilder.RequestType.MapImage)
                .SetLocationFrom("New+York,NY")
                .SetLocationTo("Washington,DC");

            await client.RequestImageAsync(req, "test.jpg");
        }

        [Test]
        public void RouteBuilderTypeTest1()
        {
            var client = new MapQuestClient();

            var req = client.GetBuilder(apiKey)
                .SetRequestType(IRequestBuilder.RequestType.MapImage);

            var resultIs = req.Build();

            Assert.That(resultIs, Contains.Substring("staticmap"));
        }

        [Test]
        public void RouteBuilderTypeTest2() 
        {
            var client = new MapQuestClient();

            var req = client.GetBuilder(apiKey)
                .SetRequestType(IRequestBuilder.RequestType.Route)
                .SetLocationFrom("Denver%2C+CO")
                .SetLocationTo("Boulder%2C+CO");

            var resultIs = req.Build();

            Assert.That(resultIs, Contains.Substring("to=Boulder%2C+CO"));
            Assert.That(resultIs, Contains.Substring("from=Denver%2C+CO"));
        }
    }
}
