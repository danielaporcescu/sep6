using FlightsWebApplication.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Services.Models.Flights;
using Services.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ServicesTest.Controllers
{
    public class FlightsControllerTest
    {
        private readonly Mock<IFlightsRepository> flightsRepository;
        private readonly FlightsController flightsController;

        public FlightsControllerTest()
        {
            flightsRepository = new Mock<IFlightsRepository>();
            flightsController = new FlightsController(flightsRepository.Object);
        }

        [Fact]
        public void CanGetFlightsPerMonth()
        {
            // Arrange
            flightsRepository
                .Setup(x => x.GetNumberOfFlightsPerMonth())
                .Returns(Task.FromResult(Mock.Of<IEnumerable<MonthFlightNumber>>()));

            // Act
            var result = flightsController.GetFlightsPerMonth();

            // Assert
            Assert.Equal(200, (result as OkObjectResult).StatusCode);
        }

        [Fact]
        public void CanGetFlightsPerMonthPerDestination()
        {
            // Arrange
            flightsRepository
                .Setup(x => x.GetNumberOfFlightsPerMonthFromDestinations())
                .Returns(Task.FromResult(Mock.Of<IEnumerable<FlightsFromOriginsPerMonth>>()));

            // Act
            var result = flightsController.GetFlightsPerMonthPerDestination();

            // Assert
            Assert.Equal(200, (result as OkObjectResult).StatusCode);
        }

        [Fact]
        public void CanGetFlightsPerMonthPerDestinationPercentage()
        {
            // Arrange
            flightsRepository
                .Setup(x => x.GetPercentageOfFlightsPerMonthFromDestinations())
                .Returns(Task.FromResult(Mock.Of<IEnumerable<FlightsFromOriginsPerMonth>>()));

            // Act
            var result = flightsController.GetFlightsPerMonthPerDestinationPercentage();

            // Assert
            Assert.Equal(200, (result as OkObjectResult).StatusCode);
        }

        [Fact]
        public void CanGetTopTenNumberOfFlights()
        {
            // Arrange
            flightsRepository
                .Setup(x => x.GetTopTenNumberOfFlights())
                .Returns(Task.FromResult(Mock.Of<IEnumerable<DestinationFlightCount>>()));

            // Act
            var result = flightsController.GetTopTenNumberOfFlights();

            // Assert
            Assert.Equal(200, (result as OkObjectResult).StatusCode);
        }

        [Fact]
        public void CanGetTopTenNumberOfFlightsForMainOrigins()
        {
            // Arrange
            flightsRepository
                .Setup(x => x.GetTopTenNumberOfFlightsForOrigins())
                .Returns(Task.FromResult(Mock.Of<IEnumerable<AirportNameMainAirportsCount>>()));

            // Act
            var result = flightsController.GetTopTenNumberOfFlightsForMainOrigins();

            // Assert
            Assert.Equal(200, (result as OkObjectResult).StatusCode);
        }

        [Fact]
        public void CanGetMeanAirTime()
        {
            // Arrange
            flightsRepository
                .Setup(x => x.GetMeanAirTime())
                .Returns(Task.FromResult(Mock.Of<MeanAirTime>()));

            // Act
            var result = flightsController.GetMeanAirTime();

            // Assert
            Assert.Equal(200, (result as OkObjectResult).StatusCode);
        }

        [Fact]
        public void CanGetOriginDelays()
        {
            // Arrange
            flightsRepository
                .Setup(x => x.GetOriginDelays())
                .Returns(Task.FromResult(Mock.Of<OriginDelays>()));

            // Act
            var result = flightsController.GetOriginDelays();

            // Assert
            Assert.Equal(200, (result as OkObjectResult).StatusCode);
        }

        [Fact]
        public void CanGetChartDate()
        {
            // Arrange
            flightsRepository
                .Setup(x => x.GetChartData())
                .Returns(Task.FromResult(Mock.Of<ChartData>()));

            // Act
            var result = flightsController.GetChartDate();

            // Assert
            Assert.Equal(200, (result as OkObjectResult).StatusCode);
        }
    }
}