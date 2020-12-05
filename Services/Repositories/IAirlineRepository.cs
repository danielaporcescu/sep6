using FlightsWebApplication.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataContext.Repositories
{
    public interface IAirlineRepository
    {
        public Task<IEnumerable<Airline>> GetAirlines();
    }
}