using Services.Models.Common;
using System.Collections.Generic;

namespace Services.Models.Weather
{
    public class ValuesForOrigins
    {
        public List<DateValue> EWRValues { get; set; } = new List<DateValue>();
        public List<DateValue> JFKValues { get; set; } = new List<DateValue>();
        public List<DateValue> LGAValues { get; set; } = new List<DateValue>();
    }
}