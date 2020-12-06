using DataContext.Context;
using Microsoft.EntityFrameworkCore;
using Services.Models.Weather;
using Services.Repositories.Interfaces;
using System.Threading.Tasks;

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
    }
}