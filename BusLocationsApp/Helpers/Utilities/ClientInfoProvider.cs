namespace BusLocationsApp.Helpers.Utilities
{
    public class ClientInfoProvider : IClientInfoProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClientInfoProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public (string IpAddress, int Port) GetClientInfo()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
                return ("0.0.0.0", 0);

            var ipAddress = httpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (string.IsNullOrEmpty(ipAddress))
                ipAddress = httpContext.Connection.RemoteIpAddress?.ToString() ?? "0.0.0.0";

            var port = httpContext.Connection.RemotePort;

            return (ipAddress, port);
        }
    }

    public interface IClientInfoProvider
    {
        (string IpAddress, int Port) GetClientInfo();
    }
}
