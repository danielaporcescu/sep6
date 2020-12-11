using Services.Models.Common;
using System.Collections.Generic;

namespace Services.Models.Weather
{
    public class WeatherChartData
    {
        public WeatherObservationsOrigin WeatherObservationsOrigin { get; set; }
        public ValuesForOrigins ValuesForOrigins { get; set; }
        public IEnumerable<DateValueCounted> DailyMeanTemperatureJFK { get; set; }
        public ValuesForOriginsCounted DailyMeanTemperatureOrigins { get; set; }
    }
}