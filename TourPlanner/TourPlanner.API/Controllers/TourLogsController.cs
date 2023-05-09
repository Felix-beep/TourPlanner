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
        public IEnumerable<TourLog> Get() => repo.GetTourLogs();

        [HttpGet("{id}")]
        public TourLog Get(int id) => repo.GetTourLogs().Single(tl => tl.ID == id);

        [HttpPost("{tourID}")]
        public void Post(int tourID, [FromBody] TourLog newTourLog) => repo.InsertTourLog(tourID, newTourLog);

        [HttpPut]
        public void Put([FromBody] TourLog updateTourLog) => repo.UpdateTourLog(updateTourLog);

        [HttpDelete("{id}")]
        public void Delete(int id) => repo.DeleteTourLog(id);
    }
}
