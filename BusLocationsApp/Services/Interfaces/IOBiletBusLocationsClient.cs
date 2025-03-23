using BusLocationsApp.Models;

namespace BusLocationsApp.Services.Interfaces
{
    public interface IOBiletBusLocationsClient
    {
        Task<BusLocationsResponse> GetLocationsAsync(string? search = null);
    }
}
