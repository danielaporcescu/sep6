using System.Collections.Generic;

namespace Services.Models.Weather
{
    public class ValuesForOrigins
    {
        public List<double?> EWRValues { get; set; } = new List<double?>();
        public List<double?> JFKValues { get; set; } = new List<double?>();
        public List<double?> LGAValues { get; set; } = new List<double?>();
    }
}