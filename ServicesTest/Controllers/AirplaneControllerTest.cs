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
    public class AirplaneControllerTest
    {
        private readonly Mock<IPlanesRepository> planesRepository;
        private readonly AirplaineController airplaineController;

        public AirplaneControllerTest()
        {
            planesRepository = new Mock<IPlanesRepository>();
            airplaineController = new AirplaineController(planesRepository.Object);
        }

        [Fact]
        public void CanGetPlanes()
        {
            // Arrange
            planesRepository.Setup(x => x.GetPlanes()).Returns(Task.FromResult(Mock.Of<IEnumerable<Plane>>())); ;

            // Act
            var result = airplaineController.GetPlanes();

            // Assert
            Assert.Equal(200, (result as OkObjectResult).StatusCode);
        }
    }
}