namespace TourPlanner.BL
{
    public interface IRequestBuilder
    {
        public enum RequestType
        {
            Route,
            MapImage
        }

        void SetRequestType(RequestType type);
        string Build();
        void Clear();
    }
}
