using log4net;
using Microsoft.AspNetCore.Mvc;
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
        readonly ILog log = LogManager.GetLogger(typeof(TourPlannerController));
        readonly IImageCache imageCache;
        readonly IRouteClient routeClient;

        public TourPlannerController(IRouteClient routeClient, IImageCache imageCache) 
        {
            this.routeClient = routeClient;
            this.imageCache = imageCache;   
        }

        [HttpGet("image/{imageName}")]
        public async Task<IActionResult> GetImageAsync(string imageName)
        {
            log.Debug($"got request: GET tourplanner/image/{imageName}");

            if (!Guid.TryParse(imageName, out var imageID))
            {
                log.Error("imageID is invalid!");
                return BadRequest();
            }

            var imageData = await imageCache.GetImageDataAsync(imageID);

            if (imageData == null)
            {
                log.Error("Failed to get image!");
                return BadRequest();
            }

            return File(imageData, "image/jpeg");
        }

        [HttpGet("image/{from}/{to}/{transportType}")]
        public async Task<IActionResult> GetImageAsync(string from, string to, TransportType transportType)
        {
            log.Debug($"got request: GET tourplanner/image/{from}/{to}/{transportType}");

            var req = routeClient.GetBuilder();
            req.SetRequestType(IRequestBuilder.RequestType.MapImage);
            req.SetLocationFrom(from);
            req.SetLocationTo(to);
            req.SetTransportType(transportType);
            var imageData = await routeClient.RequestImageDataAsync(req);
            if (imageData == null)
            {
                log.Error("Failed to get image!");
                return BadRequest();
            }

            return File(imageData, "image/jpeg");
        }

        [HttpGet("route/{from}/{to}/{transportType}")]
        public async Task<IActionResult> GetRouteAsync(string from, string to, TransportType transportType)
        {
            log.Debug($"got request: GET tourplanner/route/{from}/{to}/{transportType}");

            var result = await routeClient.RequestTourData(from, to, transportType);

            if (result == null)
            {
                log.Error("Failed to get tour data!");
                return BadRequest();
            }

            return Ok(result);
        }
    }
}
