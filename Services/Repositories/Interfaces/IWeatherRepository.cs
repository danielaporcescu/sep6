using Services.Models.Weather;
using System.Threading.Tasks;

namespace Services.Repositories.Interfaces
{
    public interface IWeatherRepository
    {
        public Task<WeatherObservationsOrigin> GetWeatherObservationsForOrigins();

        public Task<ValuesForOrigins> GetAllValuesForOrigins();
    }
}