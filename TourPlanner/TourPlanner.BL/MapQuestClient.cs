using log4net;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using TourPlanner.DAL;
using TourPlanner.Models;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace TourPlanner.BL
{
    public class MapQuestClient : IRouteClient
    {
        readonly ILog log = LogManager.GetLogger(typeof(MapQuestClient));

        readonly string apiKey;
        readonly IImageCache imageCache;
        HttpClient client = new();
        
        public MapQuestClient(IConfiguration configuration, IImageCache imageCache) 
        {
            apiKey = configuration.GetSection("ApiKeys")["MapQuestKey"];
            this.imageCache = imageCache;
            client.BaseAddress = new Uri("https://www.mapquestapi.com/");
        }

        public IRequestBuilder GetBuilder() 
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

        public async Task<Tour> RequestTourData(string from, string to, TransportType transportType)
        {
            var builder = GetBuilder()
                .SetRequestType(IRequestBuilder.RequestType.Route)
                .SetTransportType(transportType)
                .SetLocationFrom(from)
                .SetLocationTo(to);

            var json = await RequestJsonStringAsync(builder);

            var jsonData = JsonConvert.DeserializeObject<dynamic>(json);

            log.DebugFormat("got mapquest route statuscode: {0}", (int)jsonData.info.statuscode);

            if (jsonData.info.statuscode != 0)
            {
                log.Warn("mapquestclient failed to find route");
                return null;
            }

            builder.SetRequestType(IRequestBuilder.RequestType.MapImage);

            var imageData = await RequestImageDataAsync(builder);

            if (imageData == null)
            {
                return null;
            }

            var newImageID = Guid.NewGuid();
            await imageCache.SaveImageAsync(newImageID, imageData);

            return new Tour
            {
                from = from,
                to = to,
                transportType = transportType.ToString(),
                tourDistance = (double)jsonData.route.distance,
                estimatedTime = TimeSpan.FromSeconds((double)jsonData.route.time),
                imageID = newImageID.ToString()
            };
        }
    }
}
