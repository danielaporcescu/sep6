using Microsoft.AspNetCore.Mvc;
using Services.Models.Weather;
using Services.Repositories.Interfaces;

namespace FlightsWebApplication.Controllers
{
    public class WeatherController
        : Controller
    {
        private readonly IWeatherRepository weatherRepository;

        public WeatherController(IWeatherRepository weatherRepository)
        {
            this.weatherRepository = weatherRepository;
        }

        [HttpGet]
        [Route("/api/weather/count-for-main-origins")]
        [ProducesResponseType(200, Type = typeof(WeatherObservationsOrigin))]
        [ProducesResponseType(400)]
        public IActionResult GetAirlines()

        {
            var result = weatherRepository.GetWeatherObservationsForOrigins();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(result.Result);
        }
    }
}