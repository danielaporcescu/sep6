using DataContext.Context;
using FizzWare.NBuilder;
using DataContext.Entities;
using Services.Repositories;
using System.Linq;
using Xunit;

namespace ServicesTest.Repositories
{
    public class FlightsRepositoryTest
        : BaseDbTest
    {
        private readonly FlightsRepository flightsRepository;
        private readonly UAAContext context;

        public FlightsRepositoryTest()
        {
            context = factory.GetUAAContext();

            flightsRepository = new FlightsRepository(context);
        }

        [Fact]
        public async void CanGetNumberOfFlightsPerMonth()
        {
            // Arrange
            var items =
                Builder<FlightDc>
                    .CreateListOfSize(78)
                        .All()
                            .With(x => x.Year, 2013)
                        .TheFirst(1)
                            .With(x => x.Month, 1)
                        .TheNext(2)
                            .With(x => x.Month, 2)
                        .TheNext(3)
                            .With(x => x.Month, 3)
                        .TheNext(4)
                            .With(x => x.Month, 4)
                        .TheNext(5)
                            .With(x => x.Month, 5)
                        .TheNext(6)
                            .With(x => x.Month, 6)
                        .TheNext(7)
                            .With(x => x.Month, 7)
                        .TheNext(8)
                            .With(x => x.Month, 8)
                        .TheNext(9)
                            .With(x => x.Month, 9)
                        .TheNext(10)
                            .With(x => x.Month, 10)
                        .TheNext(11)
                            .With(x => x.Month, 11)
                        .TheNext(12)
                            .With(x => x.Month, 12)
                    .Build();

            context.Flights.AddRange(items);
            context.SaveChanges();

            // Act
            var result = await flightsRepository.GetNumberOfFlightsPerMonth();
            result.GroupBy(x => x.Month);

            //Assert
            Assert.Equal(1, result.ElementAt(0).NumberOfFlights);
            Assert.Equal(2, result.ElementAt(1).NumberOfFlights);
            Assert.Equal(3, result.ElementAt(2).NumberOfFlights);
            Assert.Equal(4, result.ElementAt(3).NumberOfFlights);
            Assert.Equal(5, result.ElementAt(4).NumberOfFlights);
            Assert.Equal(6, result.ElementAt(5).NumberOfFlights);
            Assert.Equal(7, result.ElementAt(6).NumberOfFlights);
            Assert.Equal(8, result.ElementAt(7).NumberOfFlights);
            Assert.Equal(9, result.ElementAt(8).NumberOfFlights);
            Assert.Equal(10, result.ElementAt(9).NumberOfFlights);
            Assert.Equal(11, result.ElementAt(10).NumberOfFlights);
            Assert.Equal(12, result.ElementAt(11).NumberOfFlights);
        }

        [Fact]
        public async void CanGetNumberOfFlightsPerMonth_NoData()
        {
            // Act
            var result = await flightsRepository.GetNumberOfFlightsPerMonth();
            result.GroupBy(x => x.Month);

            //Assert
            Assert.Equal(0, result.ElementAt(0).NumberOfFlights);
            Assert.Equal(0, result.ElementAt(1).NumberOfFlights);
            Assert.Equal(0, result.ElementAt(2).NumberOfFlights);
            Assert.Equal(0, result.ElementAt(3).NumberOfFlights);
            Assert.Equal(0, result.ElementAt(4).NumberOfFlights);
            Assert.Equal(0, result.ElementAt(5).NumberOfFlights);
            Assert.Equal(0, result.ElementAt(6).NumberOfFlights);
            Assert.Equal(0, result.ElementAt(7).NumberOfFlights);
            Assert.Equal(0, result.ElementAt(8).NumberOfFlights);
            Assert.Equal(0, result.ElementAt(9).NumberOfFlights);
            Assert.Equal(0, result.ElementAt(10).NumberOfFlights);
            Assert.Equal(0, result.ElementAt(11).NumberOfFlights);
        }

        [Fact]
        public async void CanGetNumberOfFlightsPerMonthFromDestinations()
        {
            // Arrange
            var itemsLGA =
            Builder<FlightDc>
              .CreateListOfSize(78)
                  .All()
                      .With(x => x.Year, 2013)
                  .TheFirst(1)
                      .With(x => x.Month, 1)
                      .With(x => x.Origin = "EWR")
                  .TheNext(2)
                      .With(x => x.Month, 2)
                      .With(x => x.Origin = "JFK")
                  .TheNext(3)
                      .With(x => x.Month, 3)
                      .With(x => x.Origin = "LGA")
                  .TheNext(4)
                      .With(x => x.Month, 4)
                      .With(x => x.Origin = "EWR")
                  .TheNext(5)
                      .With(x => x.Month, 5)
                      .With(x => x.Origin = "JFK")
                  .TheNext(6)
                      .With(x => x.Month, 6)
                      .With(x => x.Origin = "LGA")
                  .TheNext(7)
                      .With(x => x.Month, 7)
                      .With(x => x.Origin = "EWR")
                  .TheNext(8)
                      .With(x => x.Month, 8)
                      .With(x => x.Origin = "JFK")
                  .TheNext(9)
                      .With(x => x.Month, 9)
                      .With(x => x.Origin = "LGA")
                  .TheNext(10)
                      .With(x => x.Month, 10)
                      .With(x => x.Origin = "EWR")
                  .TheNext(11)
                      .With(x => x.Month, 11)
                      .With(x => x.Origin = "JFK")
                  .TheNext(12)
                      .With(x => x.Month, 12)
                      .With(x => x.Origin = "LGA")
              .Build();

            context.Flights.AddRange(itemsLGA);

            context.SaveChanges();
            //Act
            var result = await flightsRepository.GetNumberOfFlightsPerMonthFromDestinations();
            result.GroupBy(x => x.Month);

            //Assert
            Assert.Equal(1, result.ElementAt(0).EWR);
            Assert.Equal(2, result.ElementAt(1).JFK);
            Assert.Equal(3, result.ElementAt(2).LGA);
            Assert.Equal(4, result.ElementAt(3).EWR);
            Assert.Equal(5, result.ElementAt(4).JFK);
            Assert.Equal(6, result.ElementAt(5).LGA);
            Assert.Equal(7, result.ElementAt(6).EWR);
            Assert.Equal(8, result.ElementAt(7).JFK);
            Assert.Equal(9, result.ElementAt(8).LGA);
            Assert.Equal(10, result.ElementAt(9).EWR);
            Assert.Equal(11, result.ElementAt(10).JFK);
            Assert.Equal(12, result.ElementAt(11).LGA);
        }

        [Fact]
        public async void CanGetPercentageOfFlightsPerMonthFromDestinations()
        {
            // Arrange
            var itemsLGA =
            Builder<FlightDc>
              .CreateListOfSize(78)
                  .All()
                      .With(x => x.Year, 2013)
                  .TheFirst(1)
                      .With(x => x.Month, 1)
                      .With(x => x.Origin = "EWR")
                  .TheNext(2)
                      .With(x => x.Month, 2)
                      .With(x => x.Origin = "JFK")
                  .TheNext(3)
                      .With(x => x.Month, 3)
                      .With(x => x.Origin = "LGA")
                  .TheNext(4)
                      .With(x => x.Month, 4)
                      .With(x => x.Origin = "EWR")
                  .TheNext(5)
                      .With(x => x.Month, 5)
                      .With(x => x.Origin = "JFK")
                  .TheNext(6)
                      .With(x => x.Month, 6)
                      .With(x => x.Origin = "LGA")
                  .TheNext(7)
                      .With(x => x.Month, 7)
                      .With(x => x.Origin = "EWR")
                  .TheNext(8)
                      .With(x => x.Month, 8)
                      .With(x => x.Origin = "JFK")
                  .TheNext(9)
                      .With(x => x.Month, 9)
                      .With(x => x.Origin = "LGA")
                  .TheNext(10)
                      .With(x => x.Month, 10)
                      .With(x => x.Origin = "EWR")
                  .TheNext(11)
                      .With(x => x.Month, 11)
                      .With(x => x.Origin = "JFK")
                  .TheNext(12)
                      .With(x => x.Month, 12)
                      .With(x => x.Origin = "LGA")
              .Build();

            context.Flights.AddRange(itemsLGA);

            context.SaveChanges();
            //Act
            var result = await flightsRepository.GetPercentageOfFlightsPerMonthFromDestinations();
            result.GroupBy(x => x.Month);

            //Assert
            Assert.Equal(100, result.ElementAt(0).EWR);
            Assert.Equal(100, result.ElementAt(1).JFK);
            Assert.Equal(100, result.ElementAt(2).LGA);
            Assert.Equal(100, result.ElementAt(3).EWR);
            Assert.Equal(100, result.ElementAt(4).JFK);
            Assert.Equal(100, result.ElementAt(5).LGA);
            Assert.Equal(100, result.ElementAt(6).EWR);
            Assert.Equal(100, result.ElementAt(7).JFK);
            Assert.Equal(100, result.ElementAt(8).LGA);
            Assert.Equal(100, result.ElementAt(9).EWR);
            Assert.Equal(100, result.ElementAt(10).JFK);
            Assert.Equal(100, result.ElementAt(11).LGA);
        }

        [Fact]
        public async void CanGetTopTenNumberOfFlights()
        {
            // Arrange
            var items =
                Builder<FlightDc>
                    .CreateListOfSize(102)
                    .TheFirst(10)
                        .With(x => x.Dest = "Top1")
                    .TheNext(10)
                        .With(x => x.Dest = "Top2")
                    .TheNext(10)
                        .With(x => x.Dest = "Top3")
                    .TheNext(10)
                        .With(x => x.Dest = "Top4")
                    .TheNext(10)
                        .With(x => x.Dest = "Top5")
                    .TheNext(10)
                        .With(x => x.Dest = "Top6")
                    .TheNext(10)
                        .With(x => x.Dest = "Top7")
                    .TheNext(10)
                        .With(x => x.Dest = "Top8")
                    .TheNext(10)
                        .With(x => x.Dest = "Top9")
                    .TheNext(10)
                        .With(x => x.Dest = "Top10")
                    .TheNext(1)
                        .With(x => x.Dest = "1")
                    .TheNext(1)
                        .With(x => x.Dest = "2")
                .Build();

            context.Flights.AddRange(items);
            context.SaveChanges();

            //Act
            var result = await flightsRepository.GetTopTenNumberOfFlights();

            //Assert
            Assert.Equal(10, result.Count());

            Assert.Contains(result, x => x.Dest == "Top1");
            Assert.Contains(result, x => x.Dest == "Top2");
            Assert.Contains(result, x => x.Dest == "Top3");
            Assert.Contains(result, x => x.Dest == "Top4");
            Assert.Contains(result, x => x.Dest == "Top5");
            Assert.Contains(result, x => x.Dest == "Top6");
            Assert.Contains(result, x => x.Dest == "Top7");
            Assert.Contains(result, x => x.Dest == "Top8");
            Assert.Contains(result, x => x.Dest == "Top9");
            Assert.Contains(result, x => x.Dest == "Top10");
            Assert.DoesNotContain(result, x => x.Dest == "1");
            Assert.DoesNotContain(result, x => x.Dest == "2");
        }

        [Fact]
        public async void CanGetTopTenNumberOfFlightsForOrigins()
        {
            // Arrange
            var items =
                Builder<FlightDc>
                    .CreateListOfSize(102)
                    .TheFirst(10)
                        .With(x => x.Dest = "Top1")
                        .With(x => x.Origin = "EWR")
                    .TheNext(10)
                        .With(x => x.Dest = "Top2")
                        .With(x => x.Origin = "JFK")
                    .TheNext(10)
                        .With(x => x.Dest = "Top3")
                        .With(x => x.Origin = "LGA")
                    .TheNext(10)
                        .With(x => x.Dest = "Top4")
                        .With(x => x.Origin = "EWR")
                    .TheNext(10)
                        .With(x => x.Dest = "Top5")
                        .With(x => x.Origin = "JFK")
                    .TheNext(10)
                        .With(x => x.Dest = "Top6")
                        .With(x => x.Origin = "LGA")
                    .TheNext(10)
                        .With(x => x.Dest = "Top7")
                        .With(x => x.Origin = "EWR")
                    .TheNext(10)
                        .With(x => x.Dest = "Top8")
                        .With(x => x.Origin = "JFK")
                    .TheNext(10)
                        .With(x => x.Dest = "Top9BadOrigin")
                    .TheNext(10)
                        .With(x => x.Dest = "Top10BadOrigin")
                    .TheNext(1)
                        .With(x => x.Dest = "1")
                        .With(x => x.Origin = "LGA")
                    .TheNext(1)
                        .With(x => x.Dest = "2")
                        .With(x => x.Origin = "EWR")

                .Build();

            context.Flights.AddRange(items);
            context.SaveChanges();

            //Act
            var result = await flightsRepository.GetTopTenNumberOfFlightsForOrigins();

            //Assert
            Assert.Equal(10, result.Count());

            Assert.Contains(result, x => x.AirportName == "Top1");
            Assert.Contains(result, x => x.AirportName == "Top2");
            Assert.Contains(result, x => x.AirportName == "Top3");
            Assert.Contains(result, x => x.AirportName == "Top4");
            Assert.Contains(result, x => x.AirportName == "Top5");
            Assert.Contains(result, x => x.AirportName == "Top6");
            Assert.Contains(result, x => x.AirportName == "Top7");
            Assert.Contains(result, x => x.AirportName == "Top8");
            Assert.Contains(result, x => x.AirportName == "1");
            Assert.Contains(result, x => x.AirportName == "2");
            Assert.DoesNotContain(result, x => x.AirportName == "Top9BadOrigin");
            Assert.DoesNotContain(result, x => x.AirportName == "Top10BadOrigin");
        }

        [Fact]
        public async void CanGetMeanAirTime()
        {
            // Arange
            var items =
                Builder<FlightDc>
                    .CreateListOfSize(12)
                         .TheFirst(2)
                            .With(x => x.Origin = "EWR")
                            .With(x => x.Air_Time = 5)
                         .TheNext(2)
                            .With(x => x.Origin = "EWR")
                            .With(x => x.Air_Time = 10)
                        .TheNext(2)
                            .With(x => x.Origin = "JFK")
                            .With(x => x.Air_Time = 10)
                        .TheNext(2)
                            .With(x => x.Origin = "JFK")
                            .With(x => x.Air_Time = 15)
                        .TheNext(2)
                            .With(x => x.Origin = "LGA")
                            .With(x => x.Air_Time = 15)
                        .TheNext(2)
                            .With(x => x.Origin = "LGA")
                            .With(x => x.Air_Time = 20)
                .Build();

            context.Flights.AddRange(items);
            context.SaveChanges();

            // Act
            var result = await flightsRepository.GetMeanAirTime();

            // Assert
            Assert.Equal(7.5, result.EWR);
            Assert.Equal(12.5, result.JFK);
            Assert.Equal(17.5, result.LGA);
        }

        [Fact]
        public async void CanGetOriginDelay()
        {
            // Arrange
            var items =
                Builder<FlightDc>.
                    CreateListOfSize(30)
                        .TheFirst(5)
                            .With(x => x.Origin = "EWR")
                            .With(x => x.Arr_Delay = 10)
                            .With(x => x.Dep_Delay = 10)
                        .TheNext(5)
                            .With(x => x.Origin = "EWR")
                            .With(x => x.Arr_Delay = 20)
                            .With(x => x.Dep_Delay = 20)
                        .TheNext(5)
                            .With(x => x.Origin = "JFK")
                            .With(x => x.Arr_Delay = 10)
                            .With(x => x.Dep_Delay = 10)
                        .TheNext(5)
                            .With(x => x.Origin = "JFK")
                            .With(x => x.Arr_Delay = 20)
                            .With(x => x.Dep_Delay = 20)
                        .TheNext(5)
                            .With(x => x.Origin = "LGA")
                            .With(x => x.Arr_Delay = 10)
                            .With(x => x.Dep_Delay = 10)
                        .TheNext(5)
                            .With(x => x.Origin = "LGA")
                            .With(x => x.Arr_Delay = 20)
                            .With(x => x.Dep_Delay = 20)
                .Build();

            context.Flights.AddRange(items);
            context.SaveChanges();

            // Act
            var result = await flightsRepository.GetOriginDelays();

            // Assert
            Assert.Equal(15, result.EWRArrivalDelay);
            Assert.Equal(15, result.EWRDepartureDelay);
            Assert.Equal(15, result.JFKArrivalDelay);
            Assert.Equal(15, result.JFKDepartureDelay);
            Assert.Equal(15, result.LGAArrivalDelay);
            Assert.Equal(15, result.LGADepartureDelay);
        }
    }
}