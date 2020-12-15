using DataContext.Context;
using DataContext.Entities;
using FizzWare.NBuilder;
using Services.Helpers;
using Services.Repositories;
using System.Linq;
using Xunit;

namespace ServicesTest.Repositories
{
    public class WeatherRepositoryTest
        : BaseDbTest
    {
        private readonly WeatherRepository weatherRepository;
        private readonly UAAContext context;

        public WeatherRepositoryTest()
        {
            context = factory.GetUAAContext();
            weatherRepository = new WeatherRepository(context);
        }

        [Fact]
        public async void CanGetWeatherObservationsForOrigins()
        {
            // Arrange
            var items =
                Builder<Weather>
                    .CreateListOfSize(60)
                        .TheFirst(10)
                            .With(x => x.Origin = "EWR")
                        .TheNext(20)
                            .With(x => x.Origin = "JFK")
                        .TheNext(30)
                            .With(x => x.Origin = "LGA")
                    .Build();

            context.Weather.AddRange(items);
            context.SaveChanges();

            // Act
            var result = await weatherRepository.GetWeatherObservationsForOrigins();

            // Assert
            Assert.Equal(10, result.EWR);
            Assert.Equal(20, result.JFK);
            Assert.Equal(30, result.LGA);
        }

        [Fact]
        public async void CanGetAllValuesForOrigins()
        {
            // Arrange
            var items =
                Builder<Weather>
                    .CreateListOfSize(60)
                        .All()
                            .With(x => x.Year = 2013)
                            .With(x => x.Month = 1)
                            .With(x => x.Day = 1)
                        .TheFirst(10)
                            .With(x => x.Origin = "EWR")
                        .TheNext(20)
                            .With(x => x.Origin = "JFK")
                        .TheNext(30)
                            .With(x => x.Origin = "LGA")
                    .Build();

            context.Weather.AddRange(items);
            context.SaveChanges();

            // Act
            var result = await weatherRepository.GetAllValuesForOrigins();

            // Assert
            Assert.Equal(10, result.EWRValues.Count);
            Assert.Equal(20, result.JFKValues.Count);
            Assert.Equal(30, result.LGAValues.Count);
        }

        [Fact]
        public async void CanDailyMeanTemperatureJFK()
        {
            // Arrange
            var items =
                  Builder<Weather>
                    .CreateListOfSize(60)
                        .All()
                            .With(x => x.Year = 2013)
                            .With(x => x.Origin = "JFK")
                         .TheFirst(5)
                            .With(x => x.Month = 1)
                            .With(x => x.Day = 1)
                            .With(x => x.Temp = 15)
                         .TheNext(5)
                            .With(x => x.Month = 1)
                            .With(x => x.Day = 1)
                            .With(x => x.Temp = 20)
                        .TheNext(10)
                            .With(x => x.Month = 6)
                            .With(x => x.Day = 15)
                            .With(x => x.Temp = 20)
                        .TheNext(10)
                            .With(x => x.Month = 6)
                            .With(x => x.Day = 15)
                            .With(x => x.Temp = 25)
                        .TheNext(15)
                            .With(x => x.Month = 12)
                            .With(x => x.Day = 30)
                            .With(x => x.Temp = 25)
                        .TheNext(15)
                            .With(x => x.Month = 12)
                            .With(x => x.Day = 30)
                            .With(x => x.Temp = 30)
                    .Build();

            context.Weather.AddRange(items);
            context.SaveChanges();

            // Act
            var result = await weatherRepository.DailyMeanTemperatureJFK();
            result.OrderBy(x => x.Date);

            // Assert
            Assert.Equal(Converters.FarenheitToCelsius(17.5), result.ElementAt(0).Value);
            Assert.Equal(Converters.FarenheitToCelsius(22.5), result.ElementAt(1).Value);
            Assert.Equal(Converters.FarenheitToCelsius(27.5), result.ElementAt(2).Value);
        }

        [Fact]
        public async void CanDailyMeanTemperatureOrigins()
        {
            // Arrange
            var items =
                 Builder<Weather>
                   .CreateListOfSize(6)
                       .All()
                           .With(x => x.Year = 2013)
                        .TheFirst(1)
                           .With(x => x.Origin = "EWR")
                           .With(x => x.Month = 1)
                           .With(x => x.Day = 1)
                           .With(x => x.Temp = 15)
                        .TheNext(1)
                           .With(x => x.Origin = "EWR")
                           .With(x => x.Month = 1)
                           .With(x => x.Day = 1)
                           .With(x => x.Temp = 20)
                       .TheNext(1)
                           .With(x => x.Origin = "JFK")
                           .With(x => x.Month = 6)
                           .With(x => x.Day = 15)
                           .With(x => x.Temp = 20)
                       .TheNext(1)
                           .With(x => x.Origin = "JFK")
                           .With(x => x.Month = 6)
                           .With(x => x.Day = 15)
                           .With(x => x.Temp = 25)
                       .TheNext(1)
                           .With(x => x.Origin = "LGA")
                           .With(x => x.Month = 12)
                           .With(x => x.Day = 30)
                           .With(x => x.Temp = 25)
                       .TheNext(1)
                           .With(x => x.Origin = "LGA")
                           .With(x => x.Month = 12)
                           .With(x => x.Day = 30)
                           .With(x => x.Temp = 30)
                   .Build();

            context.Weather.AddRange(items);
            context.SaveChanges();

            // Act
            var result = await weatherRepository.DailyMeanTemperatureOrigins();

            // Assert
            Assert.Equal(-8.055555555555555, result.EWRValues.ElementAt(0).Value);
            Assert.Equal(-5.277777777777779, result.JFKValues.ElementAt(0).Value);
            Assert.Equal(-2.5, result.LGAValues.ElementAt(0).Value);
        }
    }
}