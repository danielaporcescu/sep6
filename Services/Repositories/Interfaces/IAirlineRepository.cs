using FlightsWebApplication.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Repositories.Interfaces
{
    public interface IAirlineRepository
    {
        public Task<IEnumerable<Airline>> GetAirlines();
    }
}