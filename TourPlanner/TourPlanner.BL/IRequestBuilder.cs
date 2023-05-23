namespace TourPlanner.BL
{
    public interface IRequestBuilder
    {
        public enum RequestType
        {
            None,
            Route,
            MapImage
        }

        void SetRequestType(RequestType type);
        void SetLocationFrom(string location);
        void SetLocationTo(string location);
        string? Build();
        void Clear();
    }
}
