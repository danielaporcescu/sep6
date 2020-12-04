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
6

    }
}
