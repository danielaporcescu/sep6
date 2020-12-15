using DataContext.Context;
using DataContext.Entities;
using Microsoft.EntityFrameworkCore;
using Services.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Repositories
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