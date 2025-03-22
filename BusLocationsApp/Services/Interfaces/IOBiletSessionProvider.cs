namespace BusLocationsApp.Services.Interfaces
{
    public interface IOBiletSessionProvider
    {
        Task<(string SessionId, string DeviceId)> GetOrCreateSessionAsync();
    }
}
