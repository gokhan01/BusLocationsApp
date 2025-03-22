using BusLocationsApp.Models;

namespace BusLocationsApp.Services.Interfaces
{
    public interface IOBiletJourneysClient
    {
        Task<JourneysResponse> GetJourneys(JourneysRequest journeysRequest);
    }
}
