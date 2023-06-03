using log4net;

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

        public IRequestBuilder GetBuilder(string apiKey)
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
            return await client.GetByteArrayAsync(builder.Build());
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
    }
}
