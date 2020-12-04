using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlightsWebApplication.Models
{
    public class Airport
    {
        [Key]
        public string Faa { get; set; }
        public string Name { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public double Alt { get; set; }
        public double Tz { get; set; }
        public string Dst { get; set; }
        public string Tzone { get; set; }
    }
}
