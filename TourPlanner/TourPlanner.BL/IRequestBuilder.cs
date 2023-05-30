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

        IRequestBuilder SetRequestType(RequestType type);
        IRequestBuilder SetLocationFrom(string location);
        IRequestBuilder SetLocationTo(string location);
        IRequestBuilder SetImageID(Guid imageID);
        string? Build();
        void Clear();
    }
}
