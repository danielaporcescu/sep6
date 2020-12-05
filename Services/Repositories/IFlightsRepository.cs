using FlightsWebApplication.Models;
using Services.Models;
using System.Collections.Generic;

namespace DataContext.Repositories
{
    public interface IFlightsRepository
    {
        public IEnumerable<MonthFlightNumber> GetNumberOfFlightsPerMonth();

        public IEnumerable<FlightsFromDestinationsPerMonth> GetNumberOfFlightsPerMonthFromDestinations();

        public IEnumerable<FlightsFromDestinationsPerMonth> GetPercentageOfFlightsPerMonthFromDestinations();

        //public IDictionary<string, int> GetTopTenDestinationsAndNumberOfFlights();
    }
}