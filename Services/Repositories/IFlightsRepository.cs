using FlightsWebApplication.Models;
using Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataContext.Repositories
{
    public interface IFlightsRepository
    {
        public Task<IEnumerable<MonthFlightNumber>> GetNumberOfFlightsPerMonth();

        public Task<IEnumerable<FlightsFromDestinationsPerMonth>> GetNumberOfFlightsPerMonthFromDestinations();

        public Task<IEnumerable<FlightsFromDestinationsPerMonth>> GetPercentageOfFlightsPerMonthFromDestinations();

        public Task<IEnumerable<DestinationFlightCount>> GetTopTenNumberOfFlights();

        public Task<IEnumerable<AirportNameMainAirportsCount>> GetTopTenNumberOfFlightsForMainOrigins();
    }
}