using BusLocationsApp.Models;

namespace BusLocationsApp.Services.Interfaces
{
    public interface IOBiletSessionClient
    {
        Task<SessionResponse> GetSessionAsync(SessionRequest request);
    }
}
