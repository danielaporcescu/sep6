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
                    EWR = context.Flights.Count(x => x.Origin == "EWR"),
                    JFK = context.Flights.Count(x => x.Origin == "JFK"),
                    LGA = context.Flights.Count(x => x.Origin == "LGA"),
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
    }
}