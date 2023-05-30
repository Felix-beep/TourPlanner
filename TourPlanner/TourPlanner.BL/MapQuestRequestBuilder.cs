using static TourPlanner.BL.IRequestBuilder;

namespace TourPlanner.BL
{
    public class MapQuestRequestBuilder : IRequestBuilder
    {
        const string basicRequest =
            "directions/v2/" +
            "route?key={0}&" +
            "outFormat=json&" +
            "ambiguities=ignore&" +
            "routeType=fastest&" +
            "doReverseGeocode=false&" +
            "enhancedNarrative=false&" +
            "avoidTimedConditions=false&" +
            "from={1}&" +
            "to={2}";
        
        const string basicImageRequest =
            "staticmap/v5/" +
            "map?key={0}&" +
            "start={1}&" +
            "end={2}&" +
            "size=600,400@2x";

        readonly string apiKey;

        RequestType typeParameter;
        string fromParameter;
        string toParameter;

        public MapQuestRequestBuilder(string apiKey)
        {
            this.apiKey = apiKey;
        }

        public void Clear()
        {
            typeParameter = RequestType.None;
            fromParameter = null;
            toParameter = null;
        }

        public void SetRequestType(RequestType type)
        {
            typeParameter = type;
        }

        public void SetLocationFrom(string location)
        {
            fromParameter = location;
        }

        public void SetLocationTo(string location)
        { 
            toParameter = location; 
        }

        public string? Build()
        {
            switch (typeParameter)
            {
                case RequestType.Route: return string.Format(basicRequest, apiKey, fromParameter, toParameter);
                case RequestType.MapImage: return string.Format(basicImageRequest, apiKey, fromParameter, toParameter);
            }

            return null;
        }
    }
}
