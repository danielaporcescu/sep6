namespace Services.Models
{
    public class FlightsFromOriginsPerMonth
    {
        public int Month { get; set; }
        public double EWR { get; set; } = 0;
        public double JFK { get; set; } = 0;
        public double LGA { get; set; } = 0;

        public FlightsFromOriginsPerMonth Copy()
        {
            return new FlightsFromOriginsPerMonth() { Month = this.Month, EWR = this.EWR, JFK = this.JFK, LGA = this.LGA };
        }
    }
}