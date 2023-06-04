using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Ocsp;
using TourPlanner.BL;
using TourPlanner.DAL;
using TourPlanner.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TourPlanner.API.Controllers
{
    [Route("api/tourplanner")]
    [ApiController]
    public class TourPlannerController : ControllerBase
    {
        readonly IConfiguration configuration;
        readonly IImageCache imageCache;

        MapQuestClient mapQuestClient = new();

        public TourPlannerController(IConfiguration configuration, IImageCache imageCache) 
        {
            this.configuration = configuration;
            this.imageCache = imageCache;   
        }

        [HttpGet("image/{imageName}")]
        public async Task<IActionResult> GetImageAsync(string imageName)
        {
            if (!Guid.TryParse(imageName, out var imageID))
            {
                return BadRequest();
            }

            return File(await imageCache.GetImageDataAsync(imageID), "image/jpeg");
        }

        [HttpGet("image/{from}/{to}/{transportType}")]
        public async Task<IActionResult> GetImageAsync(string from, string to, TransportType transportType)
        {
            var req = mapQuestClient.GetBuilder(configuration.GetSection("ApiKeys")["MapQuestKey"]);
            req.SetRequestType(IRequestBuilder.RequestType.MapImage);
            req.SetLocationFrom(from);
            req.SetLocationTo(to);
            req.SetTransportType(transportType);
            return File(await mapQuestClient.RequestImageDataAsync(req), "image/jpeg");
        }

        [HttpGet("route/{from}/{to}/{transportType}")]
        public async Task<IActionResult> GetRouteAsync(string from, string to, TransportType transportType)
        {
            var result = await mapQuestClient.RequestTourData(from, to, transportType, configuration.GetSection("ApiKeys")["MapQuestKey"], imageCache);

            if (result == null)
            {
                return BadRequest();
            }

            return Ok(result);
        }
    }
}
