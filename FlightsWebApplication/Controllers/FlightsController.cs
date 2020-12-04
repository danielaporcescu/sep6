using DataContext.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FlightsWebApplication.Controllers
{
    [ApiController]
    public class FlightsController
        : Controller
    {
        private readonly IFlightsRepository flightsRepository;

        public FlightsController(IFlightsRepository flightsRepository)
        {
            this.flightsRepository = flightsRepository;
        }

        [HttpGet]
        [Route("/api/flights-per-month")]
        [ProducesResponseType(200, Type = typeof(Dictionary<int, int>))]
        [ProducesResponseType(400)]
        public IActionResult GetAirlines()
        {
            var airlines = flightsRepository.GetNumberOfFlightsPerMonth();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(airlines);
        }
    }
}