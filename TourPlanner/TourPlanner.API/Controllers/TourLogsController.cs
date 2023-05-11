using log4net;
using Microsoft.AspNetCore.Mvc;
using TourPlanner.DAL;
using TourPlanner.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TourPlanner.API.Controllers
{
    [Route("api/tourlogs")]
    [ApiController]
    public class TourLogsController : ControllerBase
    {
        readonly ILog log = LogManager.GetLogger(typeof(TourLogsController));
        ITourRepository repo;

        public TourLogsController(ITourRepository repository)
        {
            repo = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<TourLog>> Get() => await repo.GetTourLogsAsync();

        [HttpGet("{id}")]
        public async Task<TourLog> Get(int id) => (await repo.GetTourLogsAsync()).Single(tl => tl.ID == id);

        [HttpPost("{tourID}")]
        public async Task<int> Post(int tourID, [FromBody] TourLog newTourLog)
        {
            return await repo.InsertTourLogAsync(tourID, newTourLog);
        }

        [HttpPut]
        public async Task Put([FromBody] TourLog updateTourLog) => await repo.UpdateTourLogAsync(updateTourLog);

        [HttpDelete("{id}")]
        public async Task Delete(int id) => await repo.DeleteTourLogAsync(id);
    }
}
