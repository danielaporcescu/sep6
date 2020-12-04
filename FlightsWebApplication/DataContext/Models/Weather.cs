using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightsWebApplication.Models
{
    public class Weather
    {
        public string Origin { get; set; }
        public DateTime Date { get; set; }
        public int Hour { get; set; }
        public double Temperature { get; set; }
        public double DewPoint { get; set; }
        public double Humidity { get; set; }
        public int WindDirection { get; set; }
        public double WindSpeed { get; set; }
        public double WindGust { get; set; }
        public int Precipitation { get; set; }
        public double Pressure { get; set; }
        public int Visibility { get; set; }
        public DateTime TimeHour { get; set; }
    }
}
