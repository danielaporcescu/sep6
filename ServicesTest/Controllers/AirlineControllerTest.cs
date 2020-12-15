using DataContext.Entities;
using FlightsWebApplication.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Services.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ServicesTest.Controllers
{
    public class AirlineControllerTest
    {
        private readonly Mock<IAirlineRepository> airlineRepository;
        private readonly AirlineController airlineController;

        public AirlineControllerTest()
        {
            airlineRepository = new Mock<IAirlineRepository>();
            airlineController = new AirlineController(airlineRepository.Object);
        }

        [Fact]
        public void CanGetAirlines()
        {
            // Arrange
            airlineRepository.Setup(x => x.GetAirlines()).Returns(Task.FromResult(Mock.Of<IEnumerable<Airline>>()));

            // Act
            var result = airlineController.GetAirlines();

            // Assert
            Assert.Equal(200, (result as OkObjectResult).StatusCode);
        }
    }
}