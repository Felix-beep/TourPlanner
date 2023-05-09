using static TourPlanner.BL.IRequestBuilder;

namespace TourPlanner.BL
{
    public class MapQuestRequestBuilder : IRequestBuilder
    {
        const string basicRequest =
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

        const string basicImageRequest = 
            "staticmap/v5/map" +
            "?key={0}" +
            "&center=New+York&size=1100,500@2x";

        readonly string apiKey;

        string request;

        public MapQuestRequestBuilder(string apiKey)
        {
            this.apiKey = apiKey;
        }

        public void Clear()
        {
            request = null;
        }

        public void SetRequestType(RequestType type)
        {
            switch (type) 
            {
                case RequestType.Route:
                    request = basicRequest;
                    break;

                case RequestType.MapImage: 
                    request = basicImageRequest; 
                    break;
            }
        }

        public string Build()
        {
            return string.Format(request, apiKey);
        }
    }
}
