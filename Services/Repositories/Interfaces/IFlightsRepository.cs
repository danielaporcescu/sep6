using Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Repositories.Interfaces
{
    public interface IFlightsRepository
    {
        public Task<IEnumerable<MonthFlightNumber>> GetNumberOfFlightsPerMonth();

        public Task<IEnumerable<FlightsFromOriginsPerMonth>> GetNumberOfFlightsPerMonthFromDestinations();

        public Task<IEnumerable<FlightsFromOriginsPerMonth>> GetPercentageOfFlightsPerMonthFromDestinations();

        public Task<IEnumerable<DestinationFlightCount>> GetTopTenNumberOfFlights();

        public Task<IEnumerable<AirportNameMainAirportsCount>> GetTopTenNumberOfFlightsForOrigins();

        public Task<MeanAirTime> GetMeanAirTime();

        public Task<ChartData> GetChartData();
    }
}