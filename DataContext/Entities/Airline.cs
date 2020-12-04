using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlightsWebApplication.Models
{
    public class Airline
    {
        [Key]
        public string Carrier { get; set; }
        public string Name { get; set; }
    }
}
