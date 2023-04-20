using log4net;

namespace TourPlanner.BL
{
    public class MapQuestClient
    {
        readonly ILog log = LogManager.GetLogger(typeof(MapQuestClient));

        HttpClient client = new HttpClient();
        
        public MapQuestClient() 
        { 
            client.BaseAddress = new Uri("https://www.mapquestapi.com/");
        }

        public IRequestBuilder GetBuilder(string apiKey) 
            => new MapQuestRequestBuilder(apiKey);

        public async Task<string> RequestJsonStringAsync(IRequestBuilder builder)
        {
            var request = builder.Build();
            log.DebugFormat("sending request: [{0}]", request);

            var response = await client.GetAsync(request);
            var content = await response.Content.ReadAsStringAsync();
            log.DebugFormat("got content: \n{0}", content);

            return content;
        }

        public async Task RequestImageAsync(IRequestBuilder builder)
        {
            var stream = await client.GetStreamAsync(builder.Build());

            using (var fs = new FileStream("test.jpg", FileMode.OpenOrCreate))
            {
                await stream.CopyToAsync(fs);
            }
        }

        public async Task<byte[]> RequestImageDataAsync(IRequestBuilder builder)
        {
            return await client.GetByteArrayAsync(builder.Build());
        }
    }
}
