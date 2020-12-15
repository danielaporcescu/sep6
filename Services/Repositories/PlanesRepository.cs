using DataContext.Context;
using DataContext.Entities;
using Microsoft.EntityFrameworkCore;
using Services.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Repositories
{
    public class PlanesRepository
        : IPlanesRepository
    {
        private readonly UAAContext context;

        public PlanesRepository(UAAContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Plane>> GetPlanes()
        {
            return await context.Planes.ToListAsync();
        }
    }
}