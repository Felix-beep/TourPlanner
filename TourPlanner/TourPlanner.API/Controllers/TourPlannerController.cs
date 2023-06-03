using Microsoft.AspNetCore.Mvc;
using TourPlanner.BL;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TourPlanner.API.Controllers
{
    [Route("api/tourplanner")]
    [ApiController]
    public class TourPlannerController : ControllerBase
    {
        const string ImageCachePath = @"imgcache/";

        readonly IConfiguration configuration;
        
        MapQuestClient mapQuestClient = new();

        public TourPlannerController(IConfiguration configuration) 
        {
            this.configuration = configuration;
        }

        [HttpGet("images/{imageName}")]
        public async Task<IActionResult> GetImageAsync(string imageName)
        {
            if (!Guid.TryParse(imageName, out var imageID))
            {
                return BadRequest();
            }

            var resultFilePath = $"{ImageCachePath}{imageID}.jpg";

            return File(await System.IO.File.ReadAllBytesAsync(resultFilePath), "image/jpeg");
        }

        [HttpGet("route/{from}/{to}")]
        public async Task<IActionResult> GetImageAsync(string from, string to)
        {
            var req = mapQuestClient.GetBuilder(configuration.GetSection("ApiKeys")["MapQuestKey"]);
            req.SetRequestType(IRequestBuilder.RequestType.MapImage);
            req.SetLocationFrom(from);
            req.SetLocationTo(to);
            return File(await mapQuestClient.RequestImageDataAsync(req), "image/jpeg");
        }
    }
}
