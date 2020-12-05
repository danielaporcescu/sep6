using DataContext.Context;
using FlightsWebApplication.Models;
using Microsoft.EntityFrameworkCore;
using Services.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataContext.Repositories
{
    public class FlightsRepository
        : IFlightsRepository
    {
        public UAAContext context;

        public FlightsRepository(UAAContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<MonthFlightNumber>> GetNumberOfFlightsPerMonth()
        {
            var list = new List<MonthFlightNumber>();

            for (int i = 1; i < 13; i++)
            {
                list.Add(new MonthFlightNumber() { Month = i, NumberOfFlights = await context.Flights.CountAsync(x => x.Month == i) });
            }

            return list;
        }

        public async Task<IEnumerable<FlightsFromDestinationsPerMonth>> GetNumberOfFlightsPerMonthFromDestinations()
        {
            var list = new List<FlightsFromDestinationsPerMonth>();

            for (int i = 1; i < 13; i++)
            {
                list.Add(new FlightsFromDestinationsPerMonth()
                {
                    Month = i,
                    EWR = await context.Flights.CountAsync(x => x.Origin == "EWR" && x.Month == i),
                    JFK = await context.Flights.CountAsync(x => x.Origin == "JFK" && x.Month == i),
                    LGA = await context.Flights.CountAsync(x => x.Origin == "LGA" && x.Month == i),
                });
            }

            return list;
        }

        public async Task<IEnumerable<FlightsFromDestinationsPerMonth>> GetPercentageOfFlightsPerMonthFromDestinations()
        {
            var list = await GetNumberOfFlightsPerMonthFromDestinations();

            foreach (var entry in list)
            {
                var total = entry.EWR + entry.JFK + entry.LGA;

                entry.EWR = entry.EWR * 100 / total;
                entry.JFK = entry.JFK * 100 / total;
                entry.LGA = entry.LGA * 100 / total;
            }

            return list;
        }

        public async Task<IEnumerable<AirportNameMainAirportsCount>> GetTopTenNumberOfFlightsForMainOrigins()
        {
            var list = new List<AirportNameMainAirportsCount>();
            await context.Flights.AsQueryable().ForEachAsync(i =>
            {
                if (!list.Any(x => x.AirportName == i.Dest))
                {
                    list.Add(new AirportNameMainAirportsCount() { AirportName = i.Dest });
                }

                switch (i.Origin)
                {
                    case "EWR":
                        list.Find(x => x.AirportName == i.Dest).EWR++;
                        break;

                    case "JFK":
                        list.Find(x => x.AirportName == i.Dest).JFK++;
                        break;

                    case "LGA":
                        list.Find(x => x.AirportName == i.Dest).LGA++;
                        break;
                }
            });

            return list.OrderByDescending(x => x.EWR + x.JFK + x.LGA).Take(10);
        }

        public async Task<IEnumerable<DestinationFlightCount>> GetTopTenNumberOfFlights()
        {
            var list = new List<DestinationFlightCount>();

            await context.Flights.AsQueryable().ForEachAsync(i =>
            {
                if (!list.Any(x => x.Dest == i.Dest))
                {
                    list.Add(new DestinationFlightCount() { Dest = i.Dest });
                }

                list.Find(x => x.Dest == i.Dest).FlightsCount++;
            });

            return list.OrderByDescending(x => x.FlightsCount).Take(10);
        }
    }
}