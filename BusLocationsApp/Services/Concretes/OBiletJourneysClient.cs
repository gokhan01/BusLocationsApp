using BusLocationsApp.Helpers;
using BusLocationsApp.Helpers.Extensions;
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
        IOptions<AppSettings> settings) : IOBiletJourneysClient
    {
        private readonly OBiletDistribusionApiOptions obiletOptions = options.Value;
        private readonly AppSettings appSettings = settings.Value;

        public async Task<JourneysResponse> GetJourneys(JourneysRequest journeysRequest)
        {
            var session = await oBiletSessionProvider.GetOrCreateSessionAsync();

            //todo: baserequest için ortak model yaratma uygulanabilir.
            BaseRequest request = new()
            {
                data = journeysRequest,
                date = DateTime.Today.ToApiFormattedString(),
                deviceSession = new DeviceSession
                {
                    deviceId = session.DeviceId,
                    sessionId = session.SessionId,
                },
                language = appSettings.DefaultCulture
            };

            var result = await genericHttpClient.PostAsync<JourneysResponse, BaseRequest>(obiletOptions.ClientName, obiletOptions.JourneysUrl, request);

            return result!;
        }
    }
}
