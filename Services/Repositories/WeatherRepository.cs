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
                    var date = new DateTime(data.Year, data.Month, data.Day);
                    date.AddHours(data.Hour);
                    var dateValue = new DateValue() { Value = Converters.FarenheitToCelsius((double)data.Temp), Date = date };
                    switch (data.Origin)
                    {
                        case "EWR":
                            result.EWRValues.Add(dateValue);
                            break;

                        case "JFK":
                            result.JFKValues.Add(dateValue);
                            break;

                        case "LGA":
                            result.LGAValues.Add(dateValue);
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
                item.Value = Converters.FarenheitToCelsius((double)item.Value) / item.Count;
            });

            return result;
        }

        public async Task<ValuesForOriginsCounted> DailyMeanTemperatureOrigins()
        {
            var result = new ValuesForOriginsCounted();

            await context.Weather.ForEachAsync(data =>
            {
                if (data.Temp != null)
                {
                    var date = new DateTime(data.Year, data.Month, data.Day);
                    date.AddHours(data.Hour);
                    var dateValue = new DateValueCounted() { Value = Converters.FarenheitToCelsius((double)data.Temp), Date = date };

                    switch (data.Origin)
                    {
                        case "EWR":
                            if (result.EWRValues.Any(x => x.Date == date))
                            {
                                var item = result.EWRValues.Find(x => x.Date == date);
                                item.Value += dateValue.Value;
                                item.Count++;
                            }
                            else
                            {
                                result.EWRValues.Add(dateValue);
                            }
                            break;

                        case "JFK":
                            if (result.JFKValues.Any(x => x.Date == date))
                            {
                                var item = result.JFKValues.Find(x => x.Date == date);
                                item.Value += dateValue.Value;
                                item.Count++;
                            }
                            else
                            {
                                result.JFKValues.Add(dateValue);
                            }
                            break;

                        case "LGA":
                            if (result.LGAValues.Any(x => x.Date == date))
                            {
                                var item = result.LGAValues.Find(x => x.Date == date);
                                item.Value += dateValue.Value;
                                item.Count++;
                            }
                            else
                            {
                                result.LGAValues.Add(dateValue);
                            }
                            break;
                    }
                }
            });

            result.EWRValues.ForEach(x =>
            {
                x.Value /= x.Count;
            });
            result.JFKValues.ForEach(x =>
            {
                x.Value /= x.Count;
            });
            result.LGAValues.ForEach(x =>
            {
                x.Value /= x.Count;
            });

            return result;
        }

        public async Task<WeatherChartData> GetWeatherChartData()
        {
            var weatherObservationsOrigin = new WeatherObservationsOrigin();
            var valuesForOrigins = new ValuesForOrigins();
            var dailyMeanTemperatureJFK = new List<DateValueCounted>();
            var dailyMeanTemperatureOrigins = new ValuesForOriginsCounted();

            await context.Weather.ForEachAsync(data =>
            {
                var date = new DateTime(data.Year, data.Month, data.Day);
                date.AddHours(data.Hour);
                if (data.Temp != null)
                {
                    var dateValue = new DateValue() { Value = Converters.FarenheitToCelsius((double)data.Temp), Date = date };

                    switch (data.Origin)
                    {
                        case "EWR":
                            weatherObservationsOrigin.EWR++;
                            valuesForOrigins.EWRValues.Add(dateValue);
                            break;

                        case "JFK":
                            weatherObservationsOrigin.JFK++;
                            valuesForOrigins.JFKValues.Add(dateValue);

                            break;

                        case "LGA":
                            weatherObservationsOrigin.LGA++;
                            valuesForOrigins.LGAValues.Add(dateValue);

                            break;
                    }
                }
                if (data.Origin == "JFK")
                {
                    if (dailyMeanTemperatureJFK.Any(x => x.Date.Year == data.Year && x.Date.Month == data.Month && x.Date.Day == data.Day))
                    {
                        var item = dailyMeanTemperatureJFK.Find(x => x.Date.Year == data.Year && x.Date.Month == data.Month && x.Date.Day == data.Day);
                        item.Count++;
                        item.Value += data.Temp;
                    }
                    else
                    {
                        dailyMeanTemperatureJFK.Add(new DateValueCounted() { Value = data.Temp, Date = new DateTime(data.Year, data.Month, data.Day) });
                    }
                }

                if (data.Temp != null)
                {
                    var dateValueCounted = new DateValueCounted() { Value = Converters.FarenheitToCelsius((double)data.Temp), Date = date };

                    switch (data.Origin)
                    {
                        case "EWR":
                            if (dailyMeanTemperatureOrigins.EWRValues.Any(x => x.Date == date))
                            {
                                var item = dailyMeanTemperatureOrigins.EWRValues.Find(x => x.Date == date);
                                item.Value += dateValueCounted.Value;
                                item.Count++;
                            }
                            else
                            {
                                dailyMeanTemperatureOrigins.EWRValues.Add(dateValueCounted);
                            }
                            break;

                        case "JFK":
                            if (dailyMeanTemperatureOrigins.JFKValues.Any(x => x.Date == date))
                            {
                                var item = dailyMeanTemperatureOrigins.JFKValues.Find(x => x.Date == date);
                                item.Value += dateValueCounted.Value;
                                item.Count++;
                            }
                            else
                            {
                                dailyMeanTemperatureOrigins.JFKValues.Add(dateValueCounted);
                            }
                            break;

                        case "LGA":
                            if (dailyMeanTemperatureOrigins.LGAValues.Any(x => x.Date == date))
                            {
                                var item = dailyMeanTemperatureOrigins.LGAValues.Find(x => x.Date == date);
                                item.Value += dateValueCounted.Value;
                                item.Count++;
                            }
                            else
                            {
                                dailyMeanTemperatureOrigins.LGAValues.Add(dateValueCounted);
                            }
                            break;
                    }
                }
            });

            dailyMeanTemperatureJFK.ForEach(item =>
            {
                item.Value = Converters.FarenheitToCelsius((double)item.Value) / item.Count;
            });

            return new WeatherChartData()
            {
                WeatherObservationsOrigin = weatherObservationsOrigin,
                ValuesForOrigins = valuesForOrigins,
                DailyMeanTemperatureJFK = dailyMeanTemperatureJFK,
                DailyMeanTemperatureOrigins = dailyMeanTemperatureOrigins
            };
        }
    }
}