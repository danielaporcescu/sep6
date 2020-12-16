using DataContext.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Data.Common;
using System.Linq;

namespace ServicesTest
{
    public class TestWebApplicationFactory<TStartup>
        : WebApplicationFactory<TStartup>
        where TStartup : class
    {
        private readonly DbConnection connection;

        public TestWebApplicationFactory()
        {
            this.connection = new SqliteConnection("Filename=:memory:");
            connection.Open();
        }

        public new void Dispose()
        {
            base.Dispose();
            connection.Dispose();
        }

        public UAAContext GetUAAContext()
        {
            //var context = new UAAContext(new DbContextOptionsBuilder<UAAContext>().UseSqlite(connection).Options);
            //context.Database.EnsureCreated();

            //return context;
            var scopeFactory = Services.GetService<IServiceScopeFactory>();
            var scope = scopeFactory.CreateScope();

            return scope.ServiceProvider.GetService<UAAContext>();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<UAAContext>));
                services.Remove(descriptor);

                services.AddDbContext<UAAContext>(options =>
                {
                    options.UseSqlite(connection);
                });
                // Init/Create db
                InitializeDb(services.BuildServiceProvider());
            });
        }

        private void InitializeDb(ServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var database = scope.ServiceProvider.GetRequiredService<UAAContext>();

            database.Database.EnsureCreated();
            database.SaveChanges();
        }
    }
}