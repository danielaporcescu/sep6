using Services.Models.Common;
using System.Collections.Generic;

namespace Services.Models.Weather
{
    public class ValuesForOriginsCounted
    {
        public List<DateValueCounted> EWRValues { get; set; } = new List<DateValueCounted>();
        public List<DateValueCounted> JFKValues { get; set; } = new List<DateValueCounted>();
        public List<DateValueCounted> LGAValues { get; set; } = new List<DateValueCounted>();
    }
}