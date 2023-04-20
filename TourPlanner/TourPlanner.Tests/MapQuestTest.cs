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
        public async Task Test()
        {
            var client = new MapQuestClient();
            var req = client.GetBuilder(apiKey);

            req.SetRequestType(IRequestBuilder.RequestType.Route);

            Assert.That(
                await client.RequestJsonStringAsync(req), 
                Contains.Substring("\"hasTunnel\":false"));
        }

        [Test]
        public void RouteBuilderTypeTest1()
        {
            var client = new MapQuestClient();
            var req = client.GetBuilder(apiKey);

            req.SetRequestType(IRequestBuilder.RequestType.MapImage);

            var resultIs = req.Build();
            var resultShouldBe =
                string.Format(
                    "staticmap/v5/map" +
                    "?key={0}" +
                    "&center=New+York&size=1100,500@2x", apiKey);

            Assert.That(resultIs, Is.EqualTo(resultShouldBe));
        }

        [Test]
        public void RouteBuilderTypeTest2() 
        {
            var client = new MapQuestClient();
            var req = client.GetBuilder(apiKey);

            req.SetRequestType(IRequestBuilder.RequestType.Route);

            var resultIs = req.Build();
            var resultShouldBe =
                string.Format(
                    "directions/v2/" +
                    "route?key={0}&" +
                    "from=Denver%2C+CO&" +
                    "to=Boulder%2C+CO&" +
                    "outFormat=json&" +
                    "ambiguities=ignore&" +
                    "routeType=fastest&" +
                    "doReverseGeocode=false&" +
                    "enhancedNarrative=false&" +
                    "avoidTimedConditions=false", 
                    apiKey);

            Assert.That(resultIs, Is.EqualTo(resultShouldBe));
        }
    }
}
