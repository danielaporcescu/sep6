using DataContext.Context;
using FlightsWebApplication.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<IEnumerable<Airline>> GetAirlines()
        {
            return await context.Airlines.AsQueryable().OrderBy(a => a.Name).ToListAsync();
        }
    }
}