namespace TourPlanner.DAL
{
    public class MapQuestClient
    {
        const string call = 
            "directions/v2/" +
            "route?key={0}&" +
            "from=Denver%2C+CO&" +
            "to=Boulder%2C+CO&" +
            "outFormat=json&" +
            "ambiguities=ignore&" +
            "routeType=fastest&" +
            "doReverseGeocode=false&" +
            "enhancedNarrative=false&" +
            "avoidTimedConditions=false";

        readonly string apiKey;
        HttpClient client = new HttpClient();
        
        public MapQuestClient(string apiKey) 
        { 
            this.apiKey = apiKey;
            client.BaseAddress = new Uri("https://www.mapquestapi.com/");
        }

        public void Request()
        {
            var request = string.Format(call, apiKey);
            Console.WriteLine($"sending request: [{request}]");

            var response = client.GetAsync(request);
            response.Wait();

            var content = response.Result.Content.ReadAsStringAsync();
            content.Wait();

            Console.WriteLine(content.Result);
        }
    }
}
