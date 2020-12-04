using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightsWebApplication.Models
{
    public class Flight
    {
        //air_time,distance,hour,minute

        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public int DepartureTime { get; set; }
        public int DepartureDelay { get; set; }
        public int ArrivalTime { get; set; }
        public int ArrivalDelay { get; set; }
        public string Carrier { get; set; }
        public string TailNumber { get; set; }
        public int Fligth { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public int AirTime { get; set; }
        public int Distance { get; set; }
    }
}
