using TourPlanner.Models;
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
            "doReverseGeocode=false&" +
            "enhancedNarrative=false&" +
            "avoidTimedConditions=false&" +
            "from={1}&" +
            "to={2}&" +
            "routeType={3}";
        
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
        TransportType transportTypeParameter = TransportType.fastest;

        public MapQuestRequestBuilder(string apiKey)
        {
            this.apiKey = apiKey;
        }

        public void Clear()
        {
            typeParameter = RequestType.None;
            fromParameter = null;
            toParameter = null;
            transportTypeParameter = TransportType.fastest;
        }

        public IRequestBuilder SetRequestType(RequestType type)
        {
            typeParameter = type;
            return this;
        }

        public IRequestBuilder SetLocationFrom(string location)
        {
            fromParameter = location;
            return this;
        }

        public IRequestBuilder SetLocationTo(string location)
        { 
            toParameter = location;
            return this;
        }

        public IRequestBuilder SetTransportType(TransportType transportType)
        {
            transportTypeParameter = transportType;
            return this;
        }

        public IRequestBuilder SetImageID(Guid imageID) 
        {
            return this;
        }

        public string? Build()
        {
            switch (typeParameter)
            {
                case RequestType.Route: 
                    return string.Format(basicRequest, apiKey, fromParameter, toParameter, transportTypeParameter);
                
                case RequestType.MapImage: 
                    return string.Format(basicImageRequest, apiKey, fromParameter, toParameter);
            }

            return null;
        }
    }
}
