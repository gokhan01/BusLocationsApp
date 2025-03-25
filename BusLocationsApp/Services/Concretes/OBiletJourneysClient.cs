using BusLocationsApp.Helpers;
using BusLocationsApp.Models;
using BusLocationsApp.Models.Configuration;
using BusLocationsApp.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace BusLocationsApp.Services.Concretes
{
    //The origin, destination and departure date provided by the user should be passed to the obilet.com business API GetJourneys method.
    public class OBiletJourneysClient(GenericHttpClient genericHttpClient,
        IOBiletSessionProvider oBiletSessionProvider,
        IOptions<OBiletDistribusionApiOptions> options,
        IOptions<AppSettings> settings) : BaseService(oBiletSessionProvider, settings.Value), IOBiletJourneysClient
    {
        private readonly OBiletDistribusionApiOptions obiletOptions = options.Value;

        public async Task<JourneysResponse> GetJourneys(JourneysRequest journeysRequest)
        {
            var request = await CreateBaseRequestAsync(journeysRequest);

            var result = await genericHttpClient.PostAsync<JourneysResponse, BaseRequest>(obiletOptions.ClientName, obiletOptions.JourneysUrl, request);

            return result!;
        }
    }
}
