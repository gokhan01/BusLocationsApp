using BusLocationsApp.Models;

namespace BusLocationsApp.Services.Interfaces
{
    public interface IOBiletBusLocationsClient
    {
        Task<BusLocationsResponse> GetLocations(string? search = null);
    }
}
