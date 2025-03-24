using System.Globalization;

namespace BusLocationsApp.Helpers.Extensions
{
    public static class DecimalExtensions
    {
        private static CultureInfo? cultureInfo;

        public static void SetCultureInfo(string culture)
        {
            cultureInfo = new CultureInfo(culture);
        }

        /// <summary>
        /// Converts decimal to string in the specified format
        /// </summary>
        public static string ToDoubledString(this decimal value, string format = "0.00")
        {
            return value.ToString(format, cultureInfo);
        }
    }
}
