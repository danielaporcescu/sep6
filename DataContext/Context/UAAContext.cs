using FlightsWebApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace DataContext.Context
{
    public class UAAContext : DbContext
    {
        public UAAContext(DbContextOptions<UAAContext> options)
            : base(options)
        {
        }

        public DbSet<Airline> Airlines { get; set; }

        //public DbSet<Airport> Airports { get; set; }
        public DbSet<FlightDc> Flights { get; set; }

        //public DbSet<Plane> Planes { get; set; }
        //public DbSet<Weather> Weather { get; set; }
    }
}