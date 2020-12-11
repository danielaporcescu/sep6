using System;
using System.ComponentModel.DataAnnotations;

namespace FlightsWebApplication.Models
{
    public class Plane
    {
        [Key]
        public string TailNum { get; set; }

        public string Year { get; set; }
        public string Type { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public int Engines { get; set; }
        public int Seats { get; set; }
        public string Speed { get; set; }
        public string Engine { get; set; }
    }
}