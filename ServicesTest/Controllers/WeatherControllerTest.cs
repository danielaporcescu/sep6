using FlightsWebApplication.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Services.Models.Common;
using Services.Models.Weather;
using Services.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ServicesTest.Controllers
{
    public class WeatherControllerTest
    {
        private readonly Mock<IWeatherRepository> weatherRepository;
        private readonly WeatherController weatherController;

        public WeatherControllerTest()
        {
            weatherRepository = new Mock<IWeatherRepository>();
            weatherController = new WeatherController(weatherRepository.Object);
        }

        [Fact]
        public void CanGetAirlines()
        {
            // Arrange
            weatherRepository
                .Setup(x => x.GetWeatherObservationsForOrigins())
                .Returns(Task.FromResult(Mock.Of<WeatherObservationsOrigin>()));

            // Act
            var result = weatherController.GetWeatherObservationsForOrigins();

            // Assert
            Assert.Equal(200, (result as OkObjectResult).StatusCode);
        }

        [Fact]
        public void CanGetAllValuesForOrigins()
        {
            // Arrange
            weatherRepository
                .Setup(x => x.GetAllValuesForOrigins())
                .Returns(Task.FromResult(Mock.Of<ValuesForOrigins>()));

            // Act
            var result = weatherController.GetAllValuesForOrigins();

            // Assert
            Assert.Equal(200, (result as OkObjectResult).StatusCode);
        }

        [Fact]
        public void CanDailyMeanTemperatureJFKs()
        {
            // Arrange
            weatherRepository
                .Setup(x => x.DailyMeanTemperatureJFK())
                .Returns(Task.FromResult(Mock.Of<IEnumerable<DateValueCounted>>()));

            // Act
            var result = weatherController.DailyMeanTemperatureJFK();

            // Assert
            Assert.Equal(200, (result as OkObjectResult).StatusCode);
        }

        [Fact]
        public void CanDailyMeanTemperatureOrigins()
        {
            // Arrange
            weatherRepository
                .Setup(x => x.DailyMeanTemperatureOrigins())
                .Returns(Task.FromResult(Mock.Of<ValuesForOriginsCounted>()));

            // Act
            var result = weatherController.DailyMeanTemperatureOrigins();

            // Assert
            Assert.Equal(200, (result as OkObjectResult).StatusCode);
        }

        [Fact]
        public void CanGetWeatherChartData()
        {
            // Arrange
            weatherRepository
                .Setup(x => x.GetWeatherChartData())
                .Returns(Task.FromResult(Mock.Of<WeatherChartData>()));

            // Act
            var result = weatherController.GetWeatherChartData();

            // Assert
            Assert.Equal(200, (result as OkObjectResult).StatusCode);
        }
    }
}