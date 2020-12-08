using DataContext.Context;
using Microsoft.EntityFrameworkCore;
using Services.Models.Weather;
using Services.Repositories.Interfaces;
using System.Threading.Tasks;
using Services.Helpers;

namespace Services.Repositories
{
    public class WeatherRepository
        : IWeatherRepository
    {
        private readonly UAAContext context;

        public WeatherRepository(UAAContext context)
        {
            this.context = context;
        }

        public async Task<WeatherObservationsOrigin> GetWeatherObservationsForOrigins()
        {
            var result = new WeatherObservationsOrigin();

            await context.Weather.ForEachAsync(data =>
             {
                 switch (data.Origin)
                 {
                     case "EWR":
                         result.EWR++;
                         break;

                     case "JFK":
                         result.JFK++;
                         break;

                     case "LGA":
                         result.LGA++;
                         break;
                 }
             });

            return result;
        }

        public async Task<ValuesForOrigins> GetAllValuesForOrigins()
        {
            var result = new ValuesForOrigins();

            await context.Weather.ForEachAsync(data =>
            {
                if (data.Temp != null)
                {
                    switch (data.Origin)
                    {
                        case "EWR":
                            result.EWRValues.Add(Converters.FarenheitToCelsius((double)data.Temp));
                            break;

                        case "JFK":
                            result.JFKValues.Add(Converters.FarenheitToCelsius((double)data.Temp));
                            break;

                        case "LGA":
                            result.LGAValues.Add(Converters.FarenheitToCelsius((double)data.Temp));
                            break;
                    }
                }
            });

            return result;
        }
    }
}