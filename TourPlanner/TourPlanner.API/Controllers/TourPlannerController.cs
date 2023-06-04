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
            if (!Guid.TryParse(imageName, out var imageID))
            {
                return BadRequest();
            }

            return File(await imageCache.GetImageDataAsync(imageID), "image/jpeg");
        }

        [HttpGet("image/{from}/{to}/{transportType}")]
        public async Task<IActionResult> GetImageAsync(string from, string to, TransportType transportType)
        {
            var req = routeClient.GetBuilder();
            req.SetRequestType(IRequestBuilder.RequestType.MapImage);
            req.SetLocationFrom(from);
            req.SetLocationTo(to);
            req.SetTransportType(transportType);
            return File(await routeClient.RequestImageDataAsync(req), "image/jpeg");
        }

        [HttpGet("route/{from}/{to}/{transportType}")]
        public async Task<IActionResult> GetRouteAsync(string from, string to, TransportType transportType)
        {
            var result = await routeClient.RequestTourData(from, to, transportType);

            if (result == null)
            {
                return BadRequest();
            }

            return Ok(result);
        }
    }
}
