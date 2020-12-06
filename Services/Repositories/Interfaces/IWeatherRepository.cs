using Services.Models.Weather;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repositories.Interfaces
{
    public interface IWeatherRepository
    {
        public Task<WeatherObservationsOrigin> GetWeatherObservationsForOrigins();
    }
}
