using UAParser;

namespace BusLocationsApp.Helpers.Utilities
{
    public class BrowserInfoProvider : IBrowserInfoProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BrowserInfoProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public (string BrowserName, string BrowserVersion) GetBrowserInfo()
        {
            var userAgent = _httpContextAccessor.HttpContext?.Request.Headers["User-Agent"].ToString();
            if (string.IsNullOrEmpty(userAgent))
                return ("Unknown", "Unknown");

            var uaParser = Parser.GetDefault();
            var clientInfo = uaParser.Parse(userAgent);

            return (
                BrowserName: clientInfo.UA.Family,
                BrowserVersion: $"{clientInfo.UA.Major}.{clientInfo.UA.Minor}.{clientInfo.UA.Patch}"
            );
        }
    }

    public interface IBrowserInfoProvider
    {
        (string BrowserName, string BrowserVersion) GetBrowserInfo();
    }
}
