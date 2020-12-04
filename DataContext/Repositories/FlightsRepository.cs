using DataContext.Context;
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

        public Dictionary<int, int> GetNumberOfFlightsPerMonth()
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
    }
}