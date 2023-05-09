using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using TourPlanner.BL;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TourPlanner.API.Controllers
{
    [Route("api/tourplanner")]
    [ApiController]
    public class TourPlannerController : ControllerBase
    {
        readonly IConfiguration configuration;

        public TourPlannerController(IConfiguration configuration) 
        {
            this.configuration = configuration;
        }

        [HttpGet("images/{imageName}")]
        public async Task<IActionResult> GetImageAsync(string imageName)
        {
            var mapQuestClient = new MapQuestClient();
            var req = mapQuestClient.GetBuilder(configuration.GetSection("ApiKeys")["MapQuestKey"]);
            req.SetRequestType(IRequestBuilder.RequestType.MapImage);
            return new FileContentResult(await mapQuestClient.RequestImageDataAsync(req), "image/jpeg");
        }

        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
