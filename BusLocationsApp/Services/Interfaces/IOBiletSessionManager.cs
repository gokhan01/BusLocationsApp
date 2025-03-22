namespace BusLocationsApp.Services.Interfaces
{
    public interface IOBiletSessionManager
    {
        void SaveSession(string sessionId, string deviceId);
        (string? SessionId, string? DeviceId) GetSession();
    }
}
