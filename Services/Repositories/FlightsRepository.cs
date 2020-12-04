using DataContext.Context;
using FlightsWebApplication.Models;
using System.Collections.Generic;

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

        public IDictionary<int, int> GetNumberOfFlightsPerMonth()
        {
            var dictionary = new Dictionary<int, int>();

            for (int i = 1; i < 13; i++)
            {
                dictionary.Add(i, 0);
            }

            foreach (var flight in context.Flights)
            {
                dictionary[flight.Month]++;
            }
            return dictionary;
        }

        public IDictionary<int, FlightsFromDestinations> GetNumberOfFlightsPerMonthFromDestinations()
        {
            var dictionary = new Dictionary<int, FlightsFromDestinations>();
            for (int i = 1; i < 13; i++)
            {
                dictionary.Add(i, new FlightsFromDestinations());
            }
            foreach (var flight in context.Flights)
            {
                switch (flight.Origin)
                {
                    case "EWR":
                        dictionary[flight.Month].EWR++;
                        break;

                    case "JFK":
                        dictionary[flight.Month].JFK++;
                        break;

                    case "LGA":
                        dictionary[flight.Month].LGA++;
                        break;

                    default: break;
                }
            }

            return dictionary;
        }

        public IDictionary<int, FlightsFromDestinations> GetPercentageOfFlightsPerMonthFromDestinations()
        {
            var flightsPerMonth = GetNumberOfFlightsPerMonthFromDestinations();

            foreach (var entry in flightsPerMonth)
            {
                var total = entry.Value.EWR + entry.Value.JFK + entry.Value.LGA;

                entry.Value.EWR = entry.Value.EWR * 100 / total;
                entry.Value.JFK = entry.Value.JFK * 100 / total;
                entry.Value.LGA = entry.Value.LGA * 100 / total;
            }

            return flightsPerMonth;
        }
    }
}