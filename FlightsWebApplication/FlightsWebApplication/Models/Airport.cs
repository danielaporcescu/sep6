using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightsWebApplication.Models
{
    public class Airport
    {
        public string FederalAviationAdministration { get; set; }
        public string Name { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public double Alt { get; set; }
        public double TimeZOffset { get; set; }
        public string Dst { get; set; }
        public string TimeZone { get; set; }
    }
}
