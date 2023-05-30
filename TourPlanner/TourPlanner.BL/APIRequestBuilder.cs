namespace TourPlanner.BL
{
    public class APIRequestBuilder : IRequestBuilder
    {
        const string baseRoute = "api/tourplanner/";

        IRequestBuilder.RequestType typeParameter;
        string fromParameter;
        string toParameter;
        Guid? imageIDParameter;

        public void Clear()
        {
            typeParameter = IRequestBuilder.RequestType.None;
            fromParameter = null;
            toParameter = null;
            imageIDParameter = null;
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

        public string? Build()
        {
            switch (typeParameter)
            {
                case IRequestBuilder.RequestType.MapImage:
                    if (imageIDParameter != null)
                    {
                        return $"{baseRoute}images/{imageIDParameter}";
                    }
                    else
                    {
                        return $"{baseRoute}route/{fromParameter}/{toParameter}";
                    }
            }

            return null;
        }
    }
}
