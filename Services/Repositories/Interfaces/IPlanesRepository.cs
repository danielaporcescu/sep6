using FlightsWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repositories.Interfaces
{
    public interface IPlanesRepository
    {
        public Task<IEnumerable<Plane>> GetPlanes();
    }
}
