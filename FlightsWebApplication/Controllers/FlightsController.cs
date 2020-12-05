using DataContext.Repositories;
using Microsoft.AspNetCore.Mvc;
using Services.Models;
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
        [ProducesResponseType(200, Type = typeof(IEnumerable<MonthFlightNumber>))]
        [ProducesResponseType(400)]
        public IActionResult GetFlightsPerMonth()
        {
            var flights = flightsRepository.GetNumberOfFlightsPerMonth();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(flights.Result);
        }

        [HttpGet]
        [Route("/api/flights-per-month-from-dest")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<FlightsFromOriginsPerMonth>))]
        [ProducesResponseType(400)]
        public IActionResult GetFlightsPerMonthPerDestination()
        {
            var flights = flightsRepository.GetNumberOfFlightsPerMonthFromDestinations();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(flights.Result);
        }

        [HttpGet]
        [Route("/api/flights-per-month-from-dest-percentage")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<FlightsFromOriginsPerMonth>))]
        [ProducesResponseType(400)]
        public IActionResult GetFlightsPerMonthPerDestinationPercentage()
        {
            var flights = flightsRepository.GetPercentageOfFlightsPerMonthFromDestinations();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(flights.Result);
        }

        [HttpGet]
        [Route("/api/top-10-dest-nr-of-flights")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<DestinationFlightCount>))]
        [ProducesResponseType(400)]
        public IActionResult GetTopTenNumberOfFlights()
        {
            var flights = flightsRepository.GetTopTenNumberOfFlights();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(flights.Result);
        }

        [HttpGet]
        [Route("/api/top-10-dest-nr-of-flights-per-main-origins")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<DestinationFlightCount>))]
        [ProducesResponseType(400)]
        public IActionResult GetTopTenNumberOfFlightsForMainOrigins()
        {
            var flights = flightsRepository.GetTopTenNumberOfFlightsForOrigins();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(flights.Result);
        }

        [HttpGet]
        [Route("/api/mean-air-time")]
        [ProducesResponseType(200, Type = typeof(MeanAirTime))]
        [ProducesResponseType(400)]
        public IActionResult GetMeanAirTime()
        {
            var flights = flightsRepository.GetMeanAirTime();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(flights.Result);
        }

        [HttpGet]
        [Route("/api/chart-data")]
        [ProducesResponseType(200, Type = typeof(ChartData))]
        [ProducesResponseType(400)]
        public IActionResult GetChartDate()
        {
            var flights = flightsRepository.GetChartData();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(flights.Result);
        }
    }
}