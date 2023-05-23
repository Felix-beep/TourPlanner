namespace TourPlanner.BL
{
    internal interface IRouteClient
    {
        IRequestBuilder GetBuilder(string apiKey);
        Task<string> RequestJsonStringAsync(IRequestBuilder builder);
        Task RequestImageAsync(IRequestBuilder builder, string fileName);
        Task<byte[]> RequestImageDataAsync(IRequestBuilder builder);
    }
}
