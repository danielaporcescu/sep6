using DataContext.Context;
using Microsoft.EntityFrameworkCore;
using Services.Models.Flights;
using Services.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Repositories
{
    public class FlightsRepository
        : IFlightsRepository
    {
        public UAAContext context;

        public FlightsRepository(UAAContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<MonthFlightNumber>> GetNumberOfFlightsPerMonth()
        {
            var list = new List<MonthFlightNumber>();

            for (int i = 1; i < 13; i++)
            {
                list.Add(new MonthFlightNumber() { Month = i, NumberOfFlights = await context.Flights.CountAsync(x => x.Month == i) });
            }

            return list;
        }

        public async Task<IEnumerable<FlightsFromOriginsPerMonth>> GetNumberOfFlightsPerMonthFromDestinations()
        {
            var list = new List<FlightsFromOriginsPerMonth>();

            await context.Flights.ForEachAsync(i =>
            {
                if (!list.Any(x => x.Month == i.Month))
                {
                    list.Add(new FlightsFromOriginsPerMonth() { Month = i.Month });
                }

                switch (i.Origin)
                {
                    case "EWR":
                        list.Find(x => x.Month == i.Month).EWR++;
                        break;

                    case "JFK":
                        list.Find(x => x.Month == i.Month).JFK++;
                        break;

                    case "LGA":
                        list.Find(x => x.Month == i.Month).LGA++;
                        break;
                }
            });

            return list;
        }

        public async Task<IEnumerable<FlightsFromOriginsPerMonth>> GetPercentageOfFlightsPerMonthFromDestinations()
        {
            var list = await GetNumberOfFlightsPerMonthFromDestinations();

            foreach (var entry in list)
            {
                var total = entry.EWR + entry.JFK + entry.LGA;

                entry.EWR = entry.EWR * 100 / total;
                entry.JFK = entry.JFK * 100 / total;
                entry.LGA = entry.LGA * 100 / total;
            }

            return list;
        }

        public async Task<IEnumerable<DestinationFlightCount>> GetTopTenNumberOfFlights()
        {
            var list = new List<DestinationFlightCount>();

            await context.Flights.ForEachAsync(i =>
            {
                if (!list.Any(x => x.Dest == i.Dest))
                {
                    list.Add(new DestinationFlightCount() { Dest = i.Dest });
                }

                list.Find(x => x.Dest == i.Dest).FlightsCount++;
            });

            return list.OrderByDescending(x => x.FlightsCount).Take(10);
        }

        public async Task<IEnumerable<AirportNameMainAirportsCount>> GetTopTenNumberOfFlightsForOrigins()
        {
            var list = new List<AirportNameMainAirportsCount>();
            await context.Flights.ForEachAsync(i =>
            {
                if (!list.Any(x => x.AirportName == i.Dest))
                {
                    list.Add(new AirportNameMainAirportsCount() { AirportName = i.Dest });
                }

                switch (i.Origin)
                {
                    case "EWR":
                        list.Find(x => x.AirportName == i.Dest).EWR++;
                        break;

                    case "JFK":
                        list.Find(x => x.AirportName == i.Dest).JFK++;
                        break;

                    case "LGA":
                        list.Find(x => x.AirportName == i.Dest).LGA++;
                        break;
                }
            });

            return list.OrderByDescending(x => x.EWR + x.JFK + x.LGA).Take(10);
        }

        public async Task<MeanAirTime> GetMeanAirTime()
        {
            var meanAirTime = new MeanAirTime();
            int ewrCount, jfkCount, lgaCount;
            ewrCount = jfkCount = lgaCount = 0;

            await context.Flights.ForEachAsync(i =>
            {
                switch (i.Origin)
                {
                    case "EWR":
                        meanAirTime.EWR += i.Air_Time ?? 0;
                        ewrCount++;
                        break;

                    case "JFK":
                        meanAirTime.JFK += i.Air_Time ?? 0;
                        jfkCount++;
                        break;

                    case "LGA":
                        meanAirTime.LGA += i.Air_Time ?? 0;
                        lgaCount++;
                        break;
                }
            });
            meanAirTime.EWR /= ewrCount;
            meanAirTime.JFK /= jfkCount;
            meanAirTime.LGA /= lgaCount;

            return meanAirTime;
        }

        public async Task<OriginDelays> GetOriginDelays()
        {
            var counted = new OriginDelaysCounted();

            await context.Flights.ForEachAsync(i =>
            {
                switch (i.Origin)
                {
                    case "EWR":
                        counted.EWRArrivalDelay.Value += i.Arr_Delay ?? 0;
                        counted.EWRArrivalDelay.Count += i.Arr_Delay != null ? 1 : 0;
                        counted.EWRDepartureDelay.Value += i.Dep_Delay ?? 0;
                        counted.EWRDepartureDelay.Count += i.Dep_Delay != null ? 1 : 0;
                        break;

                    case "JFK":
                        counted.JFKArrivalDelay.Value += i.Arr_Delay ?? 0;
                        counted.JFKArrivalDelay.Count += i.Arr_Delay != null ? 1 : 0;
                        counted.JFKDepartureDelay.Value += i.Dep_Delay ?? 0;
                        counted.JFKDepartureDelay.Count += i.Dep_Delay != null ? 1 : 0;
                        break;

                    case "LGA":
                        counted.LGAArrivalDelay.Value += i.Arr_Delay ?? 0;
                        counted.LGAArrivalDelay.Count += i.Arr_Delay != null ? 1 : 0;
                        counted.LGADepartureDelay.Value += i.Dep_Delay ?? 0;
                        counted.LGADepartureDelay.Count += i.Dep_Delay != null ? 1 : 0;
                        break;
                }
            });

            return new OriginDelays()
            {
                EWRArrivalDelay = (double)counted.EWRArrivalDelay.Value / counted.EWRArrivalDelay.Count,
                EWRDepartureDelay = (double)counted.EWRDepartureDelay.Value / counted.EWRDepartureDelay.Count,
                JFKArrivalDelay = (double)counted.JFKArrivalDelay.Value / counted.JFKArrivalDelay.Count,
                JFKDepartureDelay = (double)counted.JFKDepartureDelay.Value / counted.JFKDepartureDelay.Count,
                LGAArrivalDelay = (double)counted.LGAArrivalDelay.Value / counted.LGAArrivalDelay.Count,
                LGADepartureDelay = (double)counted.LGADepartureDelay.Value / counted.LGADepartureDelay.Count,
            };
        }

        public async Task<ChartData> GetChartData()
        {
            var flightsPerMonth = new List<MonthFlightNumber>();
            for (int i = 1; i < 13; i++)
            {
                flightsPerMonth.Add(new MonthFlightNumber() { Month = i });
            }

            var flightsPerMonthFromOrigins = new List<FlightsFromOriginsPerMonth>();

            var flightsPerMonthFromOriginPercentage = new List<FlightsFromOriginsPerMonth>();

            var topTenDestinationsByFlights = new List<DestinationFlightCount>();

            var topTenDestinationsByFlightsFromOrigins = new List<AirportNameMainAirportsCount>();

            var meanAirTime = new MeanAirTime();
            int ewrCount, jfkCount, lgaCount;
            ewrCount = jfkCount = lgaCount = 0;

            var originsDelaysCounted = new OriginDelaysCounted();

            await context.Flights.ForEachAsync(flight =>
            {
                // First requirement
                flightsPerMonth.Find(x => x.Month == flight.Month).NumberOfFlights++;

                // Second requirement
                if (!flightsPerMonthFromOrigins.Any(x => x.Month == flight.Month))
                {
                    flightsPerMonthFromOrigins.Add(new FlightsFromOriginsPerMonth() { Month = flight.Month });
                }

                switch (flight.Origin)
                {
                    case "EWR":
                        flightsPerMonthFromOrigins.Find(x => x.Month == flight.Month).EWR++;
                        break;

                    case "JFK":
                        flightsPerMonthFromOrigins.Find(x => x.Month == flight.Month).JFK++;
                        break;

                    case "LGA":
                        flightsPerMonthFromOrigins.Find(x => x.Month == flight.Month).LGA++;
                        break;
                }

                // Fourth requirement
                if (!topTenDestinationsByFlights.Any(x => x.Dest == flight.Dest))
                {
                    topTenDestinationsByFlights.Add(new DestinationFlightCount() { Dest = flight.Dest });
                }

                topTenDestinationsByFlights.Find(x => x.Dest == flight.Dest).FlightsCount++;

                // Fifth requirement
                if (!topTenDestinationsByFlightsFromOrigins.Any(x => x.AirportName == flight.Dest))
                {
                    topTenDestinationsByFlightsFromOrigins.Add(new AirportNameMainAirportsCount() { AirportName = flight.Dest });
                }

                switch (flight.Origin)
                {
                    case "EWR":
                        topTenDestinationsByFlightsFromOrigins.Find(x => x.AirportName == flight.Dest).EWR++;
                        break;

                    case "JFK":
                        topTenDestinationsByFlightsFromOrigins.Find(x => x.AirportName == flight.Dest).JFK++;
                        break;

                    case "LGA":
                        topTenDestinationsByFlightsFromOrigins.Find(x => x.AirportName == flight.Dest).LGA++;
                        break;
                }

                // Sixth requirement
                switch (flight.Origin)
                {
                    case "EWR":
                        meanAirTime.EWR += flight.Air_Time ?? 0;
                        ewrCount++;
                        break;

                    case "JFK":
                        meanAirTime.JFK += flight.Air_Time ?? 0;
                        jfkCount++;
                        break;

                    case "LGA":
                        meanAirTime.LGA += flight.Air_Time ?? 0;
                        lgaCount++;
                        break;
                }
                //
                switch (flight.Origin)
                {
                    case "EWR":
                        originsDelaysCounted.EWRArrivalDelay.Value += flight.Arr_Delay ?? 0;
                        originsDelaysCounted.EWRArrivalDelay.Count += flight.Arr_Delay != null ? 1 : 0;
                        originsDelaysCounted.EWRDepartureDelay.Value += flight.Dep_Delay ?? 0;
                        originsDelaysCounted.EWRDepartureDelay.Count += flight.Dep_Delay != null ? 1 : 0;
                        break;

                    case "JFK":
                        originsDelaysCounted.JFKArrivalDelay.Value += flight.Arr_Delay ?? 0;
                        originsDelaysCounted.JFKArrivalDelay.Count += flight.Arr_Delay != null ? 1 : 0;
                        originsDelaysCounted.JFKDepartureDelay.Value += flight.Dep_Delay ?? 0;
                        originsDelaysCounted.JFKDepartureDelay.Count += flight.Dep_Delay != null ? 1 : 0;
                        break;

                    case "LGA":
                        originsDelaysCounted.LGAArrivalDelay.Value += flight.Arr_Delay ?? 0;
                        originsDelaysCounted.LGAArrivalDelay.Count += flight.Arr_Delay != null ? 1 : 0;
                        originsDelaysCounted.LGADepartureDelay.Value += flight.Dep_Delay ?? 0;
                        originsDelaysCounted.LGADepartureDelay.Count += flight.Dep_Delay != null ? 1 : 0;
                        break;
                }
            });

            // Sixth requirement
            meanAirTime.EWR /= ewrCount;
            meanAirTime.JFK /= jfkCount;
            meanAirTime.LGA /= lgaCount;

            // Third requirement
            flightsPerMonthFromOriginPercentage = flightsPerMonthFromOrigins.Select(x => x.Copy()).ToList();
            foreach (var entry in flightsPerMonthFromOriginPercentage)
            {
                var total = entry.EWR + entry.JFK + entry.LGA;

                entry.EWR = entry.EWR * 100 / total;
                entry.JFK = entry.JFK * 100 / total;
                entry.LGA = entry.LGA * 100 / total;
            }

            var originDelays = new OriginDelays()
            {
                EWRArrivalDelay = (double)originsDelaysCounted.EWRArrivalDelay.Value / originsDelaysCounted.EWRArrivalDelay.Count,
                EWRDepartureDelay = (double)originsDelaysCounted.EWRDepartureDelay.Value / originsDelaysCounted.EWRDepartureDelay.Count,
                JFKArrivalDelay = (double)originsDelaysCounted.JFKArrivalDelay.Value / originsDelaysCounted.JFKArrivalDelay.Count,
                JFKDepartureDelay = (double)originsDelaysCounted.JFKDepartureDelay.Value / originsDelaysCounted.JFKDepartureDelay.Count,
                LGAArrivalDelay = (double)originsDelaysCounted.LGAArrivalDelay.Value / originsDelaysCounted.LGAArrivalDelay.Count,
                LGADepartureDelay = (double)originsDelaysCounted.LGADepartureDelay.Value / originsDelaysCounted.LGADepartureDelay.Count,
            };

            return new ChartData()
            {
                FlightsPerMonth = flightsPerMonth.OrderBy(x => x.Month),
                FlightsPerMonthFromOrigins = flightsPerMonthFromOrigins.OrderBy(x => x.Month),
                FlightsPerMonthFromOriginPercentage = flightsPerMonthFromOriginPercentage.OrderBy(x => x.Month),
                TopTenDestinationsByFlights = topTenDestinationsByFlights.OrderByDescending(x => x.FlightsCount).Take(10),
                TopTenDestinationsByFlightsFromOrigins = topTenDestinationsByFlightsFromOrigins.OrderByDescending(x => x.EWR + x.JFK + x.LGA).Take(10),
                MeanAirTime = meanAirTime,
                OriginDelays = originDelays
            };
        }
    }
}