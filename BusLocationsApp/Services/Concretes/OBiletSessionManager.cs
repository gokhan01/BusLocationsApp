using BusLocationsApp.Services.Interfaces;

namespace BusLocationsApp.Services.Concretes
{
    //A session should be created and maintained for each different end user visiting the application
    //using the GetSession method of obilet.com business API.
    //Each user should use his/her own session information in the subsequent API requests made by the application on behalf of that user
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
