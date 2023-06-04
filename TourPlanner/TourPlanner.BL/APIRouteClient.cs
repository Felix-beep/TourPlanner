using log4net;
using Newtonsoft.Json;
using TourPlanner.DAL;
using TourPlanner.Models;

namespace TourPlanner.BL
{
    public class APIRouteClient : IRouteClient
    {
        readonly ILog log = LogManager.GetLogger(typeof(APIRouteClient));

        HttpClient client = new();

        public APIRouteClient(Uri apiUri)
        {
            client.BaseAddress = apiUri;
        }

        public IRequestBuilder GetBuilder()
            => new APIRequestBuilder();

        public async Task RequestImageAsync(IRequestBuilder builder, string fileName)
        {
            var stream = await client.GetStreamAsync(builder.Build());

            using (var fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                await stream.CopyToAsync(fs);
            }
        }

        public async Task<byte[]> RequestImageDataAsync(IRequestBuilder builder)
        {
            try
            {
                return await client.GetByteArrayAsync(builder.Build());
            }
            catch (HttpRequestException) 
            {
                log.Error("failed to get image data");
                return null;
            }
        }

        public async Task<string> RequestJsonStringAsync(IRequestBuilder builder)
        {
            var request = builder.Build();
            log.DebugFormat("sending request: [{0}]", request);

            var response = await client.GetAsync(request);
            var content = await response.Content.ReadAsStringAsync();
            log.DebugFormat("got content: \n{0}", content);

            return content;
        }

        public async Task<Tour> RequestTourData(string from, string to, TransportType transportType)
        {
            var builder = GetBuilder()
                .SetRequestType(IRequestBuilder.RequestType.Route)
                .SetLocationFrom(from)
                .SetLocationTo(to)
                .SetTransportType(transportType);

            var response = await client.GetAsync(builder.Build());

            if (!response.IsSuccessStatusCode) return null;

            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<Tour>(content);

            return result;
        }
    }
}
