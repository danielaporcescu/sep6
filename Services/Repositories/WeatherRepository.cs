using DataContext.Context;
using Microsoft.EntityFrameworkCore;
using Services.Helpers;
using Services.Models.Common;
using Services.Models.Weather;
using Services.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IEnumerable<DateValueCounted>> DailyMeanTemperatureJFK()
        {
            var result = new List<DateValueCounted>();

            await context.Weather.ForEachAsync(data =>
            {
                if (data.Origin == "JFK")
                {
                    if (result.Any(x => x.Date.Year == data.Year && x.Date.Month == data.Month && x.Date.Day == data.Day))
                    {
                        var item = result.Find(x => x.Date.Year == data.Year && x.Date.Month == data.Month && x.Date.Day == data.Day);
                        item.Count++;
                        item.Value += data.Temp;
                    }
                    else
                    {
                        result.Add(new DateValueCounted() { Value = data.Temp, Date = new DateTime(data.Year, data.Month, data.Day) });
                    }
                }
            });

            result.ForEach(item =>
            {
                item.Value /= item.Count;
            });

            return result;
        }
    }
}