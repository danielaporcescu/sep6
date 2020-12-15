using FlightsWebApplication;

namespace ServicesTest
{
    public class BaseDbTest
    {
        protected readonly TestWebApplicationFactory<Startup> factory;

        public BaseDbTest()
        {
            this.factory = new TestWebApplicationFactory<Startup>();
        }

        public void Dispose()
        {
            factory.Dispose();
        }
    }
}