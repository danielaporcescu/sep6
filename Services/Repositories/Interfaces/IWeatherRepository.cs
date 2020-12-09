using Services.Models.Common;
using Services.Models.Weather;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Repositories.Interfaces
{
    public interface IWeatherRepository
    {
        public Task<WeatherObservationsOrigin> GetWeatherObservationsForOrigins();

        public Task<ValuesForOrigins> GetAllValuesForOrigins();

        public Task<IEnumerable<DateValueCounted>> DailyMeanTemperatureJFK();
    }
}