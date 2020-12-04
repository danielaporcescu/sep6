using System.ComponentModel.DataAnnotations;

namespace FlightsWebApplication.Models
{
    public class FlightDc
    {
        //air_time,distance,hour,minute
        [Key]
        public int Id { get; set; }

        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int? Dep_Time { get; set; }
        public int? Dep_Delay { get; set; }
        public int? Arr_Time { get; set; }
        public int? Arr_Delay { get; set; }
        public string? Carrier { get; set; }
        public string? Tailnum { get; set; }
        public int Flight { get; set; }
        public string Origin { get; set; }
        public string Dest { get; set; }
        public int? Air_Time { get; set; }
        public int Distance { get; set; }
        public int? Hour { get; set; }
        public int? Minute { get; set; }
    }
}