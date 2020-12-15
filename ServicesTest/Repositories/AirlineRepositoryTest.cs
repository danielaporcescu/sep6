using DataContext.Context;
using FizzWare.NBuilder;
using DataContext.Entities;
using Services.Repositories;
using System.Linq;
using Xunit;

namespace ServicesTest.Repositories
{
    public class AirlineRepositoryTest :
        BaseDbTest
    {
        private readonly AirlineRepository airlineRepository;
        private readonly UAAContext context;

        public AirlineRepositoryTest()
        {
            context = factory.GetUAAContext();

            airlineRepository = new AirlineRepository(context);
        }

        [Fact]
        public async void CanGetData()
        {
            // Arrange
            context.Airlines.AddRange(
                Builder<Airline>
                    .CreateListOfSize(10)
                    .Build());

            context.SaveChanges();

            // Act
            var result = await airlineRepository.GetAirlines();

            // Assert
            Assert.Equal(10, result.ToList().Count);
        }

        [Fact]
        public async void CanGetData_NoDate()
        {
            // Act
            var result = await airlineRepository.GetAirlines();

            // Assert
            Assert.Empty(result.ToList());
        }
    }
}