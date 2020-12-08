namespace Services.Helpers
{
    public static class Converters
    {
        public static double FarenheitToCelsius(double f)
        {
            return 5.0 / 9.0 * (f - 32);
        }
    }
}