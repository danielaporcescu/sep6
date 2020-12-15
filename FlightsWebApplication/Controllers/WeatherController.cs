using Microsoft.AspNetCore.Mvc;
using Services.Models.Common;
using Services.Models.Weather;
using Services.Repositories.Interfaces;
using System.Collections.Generic;

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
        public IActionResult GetWeatherObservationsForOrigins()

        {
            var result = weatherRepository.GetWeatherObservationsForOrigins();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(result.Result);
        }

        [HttpGet]
        [Route("/api/weather/values-for-main-origins")]
        [ProducesResponseType(200, Type = typeof(ValuesForOrigins))]
        [ProducesResponseType(400)]
        public IActionResult GetAllValuesForOrigins()

        {
            var result = weatherRepository.GetAllValuesForOrigins();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(result.Result);
        }

        [HttpGet]
        [Route("/api/weather/jfk-mean-temp-daily")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<DateValueCounted>))]
        [ProducesResponseType(400)]
        public IActionResult DailyMeanTemperatureJFK()

        {
            var result = weatherRepository.DailyMeanTemperatureJFK();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(result.Result);
        }

        [HttpGet]
        [Route("/api/weather/mean-temp-daily-origin")]
        [ProducesResponseType(200, Type = typeof(ValuesForOriginsCounted))]
        [ProducesResponseType(400)]
        public IActionResult DailyMeanTemperatureOrigins()
        {
            var result = weatherRepository.DailyMeanTemperatureOrigins();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(result.Result);
        }

        [HttpGet]
        [Route("/api/weather/chart-data")]
        [ProducesResponseType(200, Type = typeof(WeatherChartData))]
        [ProducesResponseType(400)]
        public IActionResult GetWeatherChartData()
        {
            var result = weatherRepository.GetWeatherChartData();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(result.Result);
        }
    }
}