using Services.Helpers;
using Xunit;

namespace ServicesTest.Helpers
{
    public class ConvertersTest
    {
        [Fact]
        public void CanFarenheitToCelsius()
        {
            // Assert
            Assert.Equal(10, Converters.FarenheitToCelsius(50));
        }
    }
}