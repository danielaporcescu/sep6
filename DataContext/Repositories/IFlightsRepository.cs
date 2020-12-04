using System.Collections.Generic;

namespace DataContext.Repositories
{
    public interface IFlightsRepository
    {
        public Dictionary<int, int> GetNumberOfFlightsPerMonth();
    }
}