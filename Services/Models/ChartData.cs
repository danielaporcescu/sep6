using System.Collections.Generic;

namespace Services.Models
{
    public class ChartData
    {
        public IEnumerable<MonthFlightNumber> FlightsPerMonth { get; set; }
        public IEnumerable<FlightsFromOriginsPerMonth> FlightsPerMonthFromOrigins { get; set; }
        public IEnumerable<FlightsFromOriginsPerMonth> FlightsPerMonthFromOriginPercentage { get; set; }
        public IEnumerable<DestinationFlightCount> TopTenDestinationsByFlights { get; set; }
        public IEnumerable<AirportNameMainAirportsCount> TopTenDestinationsByFlightsFromOrigins { get; set; }
        public MeanAirTime MeanAirTime { get; set; }
    }
}