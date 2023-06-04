using log4net;
using Microsoft.AspNetCore.Mvc;
using TourPlanner.DAL;
using TourPlanner.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TourPlanner.API.Controllers
{
    [Route("api/tours")]
    [ApiController]
    public class TourController : ControllerBase
    {
        readonly ILog log = LogManager.GetLogger(typeof(TourController));
        readonly ITourRepository repo;

        public TourController(ITourRepository repository)
        {
            repo = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<Tour>> Get()
        {
            log.Debug("got request: GET tours/");
            return await repo.GetToursAsync();
        }

        [HttpGet("{tourID}")]
        public async Task<Tour> Get(int tourID)
        {
            log.Debug($"got request: GET tours/{tourID}");
            return await repo.GetTourAsync(tourID);
        }

        [HttpPost]
        public async Task<int> Post([FromBody] Tour newTour)
        {
            log.Debug("got request: POST tours/");
            return await repo.InsertTourAsync(newTour);
        }

        [HttpPatch]
        public async Task Patch([FromBody] Tour updateTour)
        {
            log.Debug($"got request: PATCH tours/ with body {updateTour.CustomToString()}");
            await repo.UpdateTourAsync(updateTour);
        }

        [HttpDelete("{tourID}")]
        public async Task Delete(int tourID)
        {
            log.Debug($"got request: DELETE tours/{tourID}");
            await repo.DeleteTourAsync(tourID);
        }
    }
}
