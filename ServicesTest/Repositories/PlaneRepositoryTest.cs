using DataContext.Context;
using FizzWare.NBuilder;
using FlightsWebApplication.Models;
using Services.Repositories;
using System.Linq;
using Xunit;

namespace ServicesTest.Repositories
{
    public class PlaneRepositoryTest
        : BaseDbTest
    {
        private readonly PlanesRepository planesRepository;
        private readonly UAAContext context;

        public PlaneRepositoryTest()
        {
            context = factory.GetUAAContext();

            planesRepository = new PlanesRepository(context);
        }

        [Fact]
        public async void CanGetPlanes()
        {
            // Arrange
            context.Planes.AddRange(
                Builder<Plane>
                    .CreateListOfSize(100)
                    .Build());

            context.SaveChanges();

            // Act
            var result = await planesRepository.GetPlanes();

            // Assert
            Assert.Equal(100, result.Count());
        }

        [Fact]
        public async void CanGetPlanes_NoData()
        {
            // Act
            var result = await planesRepository.GetPlanes();

            // Assert
            Assert.Empty(result);
        }
    }
}
