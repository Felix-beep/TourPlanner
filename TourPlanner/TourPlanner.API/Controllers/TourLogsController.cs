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
        public async Task<IEnumerable<TourLog>> Get()
        {
            log.Debug("got request: GET tourlogs/");
            return await repo.GetTourLogsAsync();
        }

        [HttpGet("{tourLogID}")]
        public async Task<TourLog> Get(int tourLogID)
        {
            log.Debug($"got request: GET tourlogs/{tourLogID}");
            return await repo.GetTourLogAsync(tourLogID);
        }

        [HttpPost("{tourID}")]
        public async Task<int> Post(int tourID, [FromBody] TourLog newTourLog)
        {
            log.Debug($"got request: POST tourlogs/{tourID}");
            return await repo.InsertTourLogAsync(tourID, newTourLog);
        }

        [HttpPatch]
        public async Task Patch([FromBody] TourLog updateTourLog)
        {
            log.Debug("got request: PATCH tourlogs/");
            await repo.UpdateTourLogAsync(updateTourLog);
        }

        [HttpDelete("{tourLogID}")]
        public async Task Delete(int tourLogID)
        {
            log.Debug($"got request: DELETE tourlogs/{tourLogID}");
            await repo.DeleteTourLogAsync(tourLogID);
        }
    }
}
