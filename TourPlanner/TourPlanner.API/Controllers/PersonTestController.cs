using log4net;
using Microsoft.AspNetCore.Mvc;
using TourPlanner.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TourPlanner.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonTestController : ControllerBase
    {
        readonly ILog log = LogManager.GetLogger(typeof(PersonTestController));

        // GET: api/<PersonTestController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            log.Debug("received get request");
            return new string[] { "value1", "value2" };
        }

        // GET api/<PersonTestController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            log.Debug("received get request");
            return Ok(new PersonTestModel { Id = id, Name = "Max Muster" });
        }

        // POST api/<PersonTestController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<PersonTestController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PersonTestController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
