namespace FlightsWebApplication.Models
{
    public class FlightsFromDestinationsPerMonth
    {
        public int Month { get; set; }
        public double EWR { get; set; } = 0;
        public double JFK { get; set; } = 0;
        public double LGA { get; set; } = 0;
    }
}