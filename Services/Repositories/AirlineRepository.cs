using DataContext.Context;
using FlightsWebApplication.Models;
using System.Collections.Generic;
using System.Linq;

namespace DataContext.Repositories
{
    public class AirlineRepository
        : IAirlineRepository
    {
        private readonly UAAContext context;

        public AirlineRepository(UAAContext context)
        {
            this.context = context;
        }

        public IEnumerable<Airline> GetAirlines()
        {
            return context.Airlines.OrderBy(a => a.Name).ToList();
        }
    }
}