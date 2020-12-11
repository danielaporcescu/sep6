using Services.Models.Common;

namespace Services.Models.Flights
{
    public class OriginDelaysCounted
    {
        public CountedValue EWRDepartureDelay { get; set; } = new CountedValue();
        public CountedValue EWRArrivalDelay { get; set; } = new CountedValue();
        public CountedValue JFKDepartureDelay { get; set; } = new CountedValue();
        public CountedValue JFKArrivalDelay { get; set; } = new CountedValue();
        public CountedValue LGADepartureDelay { get; set; } = new CountedValue();
        public CountedValue LGAArrivalDelay { get; set; } = new CountedValue();
    }
}