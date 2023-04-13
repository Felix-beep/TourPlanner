using Microsoft.Extensions.Configuration;
using TourPlanner.DAL;

namespace TourPlanner.Tests
{
    public class MapQuestTest
    {
        IConfiguration config;

        [OneTimeSetUp]
        public void LoadConfig()
        {
            config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
        }

        [Test]
        public void Test()
        {
            var client = new MapQuestClient(config.GetSection("ApiKeys")["MapQuestKey"]);
            client.Request();
        }
    }
}
