using TourPlanner.Models;

namespace TourPlanner.BL
{
    public class APIRequestBuilder : IRequestBuilder
    {
        const string baseRoute = "api/tourplanner/";

        IRequestBuilder.RequestType typeParameter;
        string fromParameter;
        string toParameter;
        TransportType transportTypeParameter = TransportType.fastest;
        Guid? imageIDParameter;

        public void Clear()
        {
            typeParameter = IRequestBuilder.RequestType.None;
            fromParameter = null;
            toParameter = null;
            imageIDParameter = null;
            transportTypeParameter = TransportType.fastest;
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

        public IRequestBuilder SetRequestType(IRequestBuilder.RequestType type)
        {
            typeParameter = type;
            return this;
        }

        public IRequestBuilder SetImageID(Guid imageID)
        {
            imageIDParameter = imageID;
            return this;
        }

        public IRequestBuilder SetTransportType(TransportType transportType)
        {
            transportTypeParameter = transportType;
            return this;
        }

        public string? Build()
        {
            switch (typeParameter)
            {
                case IRequestBuilder.RequestType.MapImage:
                    if (imageIDParameter != null)
                    {
                        return $"{baseRoute}image/{imageIDParameter}";
                    }
                    else
                    {
                        return $"{baseRoute}image/{fromParameter}/{toParameter}/{transportTypeParameter}";
                    }
                case IRequestBuilder.RequestType.Route:
                    {
                        return $"{baseRoute}route/{fromParameter}/{toParameter}/{transportTypeParameter}";
                    }
            }

            return null;
        }
    }
}
