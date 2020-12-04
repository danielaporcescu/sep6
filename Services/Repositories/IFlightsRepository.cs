using FlightsWebApplication.Models;
using System.Collections.Generic;

namespace DataContext.Repositories
{
    public interface IFlightsRepository
    {
        public IDictionary<int, int> GetNumberOfFlightsPerMonth();

        public IDictionary<int, FlightsFromDestinations> GetNumberOfFlightsPerMonthFromDestinations();

        public IDictionary<int, FlightsFromDestinations> GetPercentageOfFlightsPerMonthFromDestinations();
    }
}