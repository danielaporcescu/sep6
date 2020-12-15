using DataContext.Context;
using Microsoft.EntityFrameworkCore;

namespace ServicesTest
{
    public class TestContext
    {
        public TestContext(DbContextOptions<UAAContext> contextOptions)
        {
            ContextOptions = contextOptions;
        }

        public DbContextOptions<UAAContext> ContextOptions { get; }
    }
}