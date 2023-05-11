﻿using log4net;
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
        public async Task<IEnumerable<Tour>> Get()
        {
            log.Info("got request: GET tours/");
            return await repo.GetToursAsync();
        }

        [HttpGet("{id}")]
        public async Task<Tour> Get(int id)
        {
            log.Info($"got request: GET tours/{id}");
            return (await repo.GetToursAsync()).Single(t => t.ID == id);
        }

        [HttpPost]
        public async Task<int> Post([FromBody] Tour newTour)
        {
            log.Info("got request: POST tours/");
            return await repo.InsertTourAsync(newTour);
        }

        [HttpPatch]
        public async Task Patch([FromBody] Tour updateTour)
        {
            log.Info($"got request: PATCH tours/ with body {updateTour.CustomToString()}");
            await repo.UpdateTourAsync(updateTour);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            log.Info($"got request: DELETE tours/{id}");
            await repo.DeleteTourAsync(id);
        }
    }
}
