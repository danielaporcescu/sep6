using FlightsWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContext.Repositories
{
    public interface IAirlineRepository
    {
        public IEnumerable<Airline> GetAirlines();
    }
}
