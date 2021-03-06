﻿using DataContext.Entities;
using Microsoft.AspNetCore.Mvc;
using Services.Repositories.Interfaces;
using System.Collections.Generic;

namespace FlightsWebApplication.Controllers
{
    [ApiController]
    public class AirlineController
        : Controller
    {
        private readonly IAirlineRepository airlineRepository;

        public AirlineController(IAirlineRepository airlineRepository)
        {
            this.airlineRepository = airlineRepository;
        }

        [HttpGet]
        [Route("/api/airlines")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Airline>))]
        [ProducesResponseType(400)]
        public IActionResult GetAirlines()

        {
            var result = airlineRepository.GetAirlines();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(result.Result);
        }
    }
}