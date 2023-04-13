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
        ITourRepository repo;

        public TourController(ITourRepository repository)
        {
            repo = repository;
        }

        [HttpGet]
        public IEnumerable<Tour> Get() => repo.GetTours();

        [HttpGet("{id}")]
        public Tour Get(int id) => repo.GetTours().Single(t => t.ID == id);

        [HttpPost]
        public void Post([FromBody] Tour newTour) => repo.InsertTour(newTour);

        [HttpPatch]
        public void Patch([FromBody] Tour updateTour) => repo.UpdateTour(updateTour);

        [HttpDelete("{id}")]
        public void Delete(int id) => repo.DeleteTour(id);
    }
}
