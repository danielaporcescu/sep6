using System;

namespace FlightsWebApplication.Models
{
    public class Plane
    {
        public string TailNumber { get; set; }
        public DateTime Year { get; set; }
        public string Type { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public int Engines { get; set; }
        public int Seats { get; set; }
        public string Speed { get; set; }
        public string EngineType { get; set; }
    }
}