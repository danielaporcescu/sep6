﻿using DataContext.Repositories;
using FlightsWebApplication.Models;
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
        public IActionResult GetFlightsPerMonth()
        {
            var flights = flightsRepository.GetNumberOfFlightsPerMonth();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(flights);
        }

        [HttpGet]
        [Route("/api/flights-per-month-from-dest")]
        [ProducesResponseType(200, Type = typeof(Dictionary<int, FlightsFromDestinations>))]
        [ProducesResponseType(400)]
        public IActionResult GetFlightsPerMonthPerDestination()
        {
            var flights = flightsRepository.GetNumberOfFlightsPerMonthFromDestinations();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(flights);
        }

        [HttpGet]
        [Route("/api/flights-per-month-from-dest-percentage")]
        [ProducesResponseType(200, Type = typeof(Dictionary<int, FlightsFromDestinations>))]
        [ProducesResponseType(400)]
        public IActionResult GetFlightsPerMonthPerDestinationPercentage()
        {
            var flights = flightsRepository.GetPercentageOfFlightsPerMonthFromDestinations();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(flights);
        }
    }
}