using Microsoft.AspNetCore.Mvc;
using Services.Models.Weather;
using Services.Repositories.Interfaces;

namespace FlightsWebApplication.Controllers
{
    public class AirplaineController
        : ControllerBase
    {
        private readonly IPlanesRepository repository;

        public AirplaineController(IPlanesRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        [Route("/api/airplanes/data")]
        [ProducesResponseType(200, Type = typeof(WeatherObservationsOrigin))]
        [ProducesResponseType(400)]
        public IActionResult GetPlanes()

        {
            var result = repository.GetPlanes();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(result.Result);
        }
    }
}