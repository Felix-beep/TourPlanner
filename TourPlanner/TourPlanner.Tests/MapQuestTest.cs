using TourPlanner.DAL;

namespace TourPlanner.Tests
{
    public class MapQuestTest
    {
        [Test]
        public void Test()
        {
            var client = new MapQuestClient("");
            client.Request();
        }
    }
}
