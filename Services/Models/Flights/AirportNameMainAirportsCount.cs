namespace Services.Models
{
    public class AirportNameMainAirportsCount
    {
        public string AirportName { get; set; }
        public double EWR { get; set; } = 0;
        public double JFK { get; set; } = 0;
        public double LGA { get; set; } = 0;
    }
}