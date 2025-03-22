using BusLocationsApp.Services.Interfaces;

namespace BusLocationsApp.Services.Concretes
{
    public class OBiletSessionManager(IHttpContextAccessor httpContextAccessor) : IOBiletSessionManager
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public (string? SessionId, string? DeviceId) GetSession()
        {
            var session = _httpContextAccessor.HttpContext?.Session;
            return (
                session?.GetString("SessionId"),
                session?.GetString("DeviceId")
            );
        }

        public void SaveSession(string sessionId, string deviceId)
        {
            var session = _httpContextAccessor.HttpContext?.Session;
            session?.SetString("SessionId", sessionId);
            session?.SetString("DeviceId", deviceId);
        }
    }
}
