using BusLocationsApp.Helpers;
using BusLocationsApp.Models;
using BusLocationsApp.Models.Configuration;
using BusLocationsApp.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace BusLocationsApp.Services.Concretes
{
    public class OBiletJourneysClient(GenericHttpClient genericHttpClient, IOBiletSessionProvider oBiletSessionProvider, IOptions<OBiletDistribusionApiOptions> options) : IOBiletJourneysClient
    {
        private readonly OBiletDistribusionApiOptions obiletOptions = options.Value;

        public async Task<JourneysResponse> GetJourneys(JourneysRequest journeysRequest)
        {
            var session = await oBiletSessionProvider.GetOrCreateSessionAsync();

            //todo: baserequest için ortak model yaratma uygulanabilir.
            BaseRequest request = new()
            {
                data = journeysRequest,
                date = DateTime.Today.ToString("yyyy-MM-dd"),
                deviceSession = new DeviceSession
                {
                    deviceId = session.DeviceId,
                    sessionId = session.SessionId,
                },
                language = "tr-TR"
            };

            var result = await genericHttpClient.PostAsync<JourneysResponse, BaseRequest>(obiletOptions.ClientName, obiletOptions.JourneysUrl, request);

            return result!;
        }
    }
}
