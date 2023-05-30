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

        public void SetLocationFrom(string location)
        {
            fromParameter = location;
        }

        public void SetLocationTo(string location)
        {
            toParameter = location;
        }

        public void SetRequestType(IRequestBuilder.RequestType type)
        {
            typeParameter = type;
        }

        public void SetImageID(Guid imageID)
        {
            imageIDParameter = imageID;
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
