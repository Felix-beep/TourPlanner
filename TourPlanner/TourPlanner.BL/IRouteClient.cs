using TourPlanner.DAL;
using TourPlanner.Models;

namespace TourPlanner.BL
{
    public interface IRouteClient
    {
        IRequestBuilder GetBuilder();
        Task<string> RequestJsonStringAsync(IRequestBuilder builder);
        Task<Tour> RequestTourData(string from, string to, TransportType transportType);
        Task RequestImageAsync(IRequestBuilder builder, string fileName);
        Task<byte[]> RequestImageDataAsync(IRequestBuilder builder);
    }
}
