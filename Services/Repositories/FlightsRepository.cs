using DataContext.Context;
using FlightsWebApplication.Models;
using Services.Models;
using System.Collections.Generic;
using System.Linq;

namespace DataContext.Repositories
{
    public class FlightsRepository
        : IFlightsRepository
    {
        public UAAContext context;

        public FlightsRepository(UAAContext context)
        {
            this.context = context;
        }

        public IEnumerable<MonthFlightNumber> GetNumberOfFlightsPerMonth()
        {
            var list = new List<MonthFlightNumber>();

            for (int i = 1; i < 13; i++)
            {
                list.Add(new MonthFlightNumber() { Month = i, NumberOfFlights = context.Flights.Count(x => x.Month == i) });
            }

            return list;
        }

        public IEnumerable<FlightsFromDestinationsPerMonth> GetNumberOfFlightsPerMonthFromDestinations()
        {
            var list = new List<FlightsFromDestinationsPerMonth>();

            for (int i = 1; i < 13; i++)
            {
                list.Add(new FlightsFromDestinationsPerMonth()
                {
                    Month = i,
                    EWR = context.Flights.Count(x => x.Origin == "EWR" && x.Month == i),
                    JFK = context.Flights.Count(x => x.Origin == "JFK" && x.Month == i),
                    LGA = context.Flights.Count(x => x.Origin == "LGA" && x.Month == i),
                });
            }

            return list;
        }

        public IEnumerable<FlightsFromDestinationsPerMonth> GetPercentageOfFlightsPerMonthFromDestinations()
        {
            var list = GetNumberOfFlightsPerMonthFromDestinations();

            foreach (var entry in list)
            {
                var total = entry.EWR + entry.JFK + entry.LGA;

                entry.EWR = entry.EWR * 100 / total;
                entry.JFK = entry.JFK * 100 / total;
                entry.LGA = entry.LGA * 100 / total;
            }

            return list;
        }

        public IEnumerable<AirportNameMainAirportsCount> GetTopTenNumberOfFlightsForMainOrigins()
        {
            var list = new List<AirportNameMainAirportsCount>();

            foreach (var flight in context.Flights)
            {
                if (!list.Any(x => x.AirportName == flight.Dest))
                {
                    list.Add(new AirportNameMainAirportsCount() { AirportName = flight.Dest });
                }

                switch (flight.Origin)
                {
                    case "EWR":
                        list.Find(x => x.AirportName == flight.Dest).EWR++;
                        break;

                    case "JFK":
                        list.Find(x => x.AirportName == flight.Dest).JFK++;
                        break;

                    case "LGA":
                        list.Find(x => x.AirportName == flight.Dest).LGA++;
                        break;
                }
            }

            return list.OrderByDescending(x => x.EWR + x.JFK + x.LGA).Take(10);
        }

        public IEnumerable<DestinationFlightCount> GetTopTenNumberOfFlights()
        {
            var list = new List<DestinationFlightCount>();
            foreach (var flight in context.Flights)
            {
                if (!list.Any(x => x.Dest == flight.Dest))
                {
                    list.Add(new DestinationFlightCount() { Dest = flight.Dest });
                }

                list.Find(x => x.Dest == flight.Dest).FlightsCount++;
            }

            return list.OrderByDescending(x => x.FlightsCount).Take(10);
        }
    }
}