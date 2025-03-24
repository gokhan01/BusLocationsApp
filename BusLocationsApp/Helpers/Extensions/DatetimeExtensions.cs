using System.Globalization;

namespace BusLocationsApp.Helpers.Extensions
{
    public static class DatetimeExtensions
    {
        private static CultureInfo? cultureInfo;

        public static void SetCultureInfo(string culture)
        {
            cultureInfo = new CultureInfo(culture);
        }

        /// <summary>
        /// Converts DateTime to string in the specified format
        /// </summary>
        public static string ToFormattedString(this DateTime date, string format = DateTimeFormats.NumericDate)
        {
            return date.ToString(format, cultureInfo);
        }

        /// <summary>
        /// Converts DateTime to string in the specified format
        /// </summary>
        public static string ToApiFormattedString(this DateTime date, string format = DateTimeFormats.ReverseNumericDate)
        {
            return date.ToString(format, cultureInfo);
        }

        /// <summary>
        /// Converts DateTime to string in the specified format
        /// </summary>
        public static string ToHourString(this DateTime date, string format = DateTimeFormats.HourDate)
        {
            return date.ToString(format);
        }

        /// <summary>
        /// Adds the specified number of days and returns a formatted string
        /// </summary>
        public static string AddDaysFormatted(this DateTime date, int days, string format = DateTimeFormats.NumericDate)
        {
            return date.AddDays(days).ToFormattedString(format);
        }
    }
}
