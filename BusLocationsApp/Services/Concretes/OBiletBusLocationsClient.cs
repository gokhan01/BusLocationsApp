using BusLocationsApp.Helpers;
using BusLocationsApp.Helpers.Extensions;
using BusLocationsApp.Models;
using BusLocationsApp.Models.Configuration;
using BusLocationsApp.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace BusLocationsApp.Services.Concretes
{
    //All possible bus locations available should fetched from the obilet.com business API GetBusLocations method
    //and be listed as available origins and destinations
    public class OBiletBusLocationsClient(GenericHttpClient genericHttpClient, 
        IOBiletSessionProvider oBiletSessionProvider, 
        IOptions<OBiletDistribusionApiOptions> options,
        IOptions<AppSettings> settings) : IOBiletBusLocationsClient
    {
        private readonly OBiletDistribusionApiOptions obiletOptions = options.Value;
        private readonly AppSettings appSettings = settings.Value;

        public async Task<BusLocationsResponse> GetLocationsAsync(string? search = null)
        {
            var session = await oBiletSessionProvider.GetOrCreateSessionAsync();

            BaseRequest request = new()
            {
                data = search,
                date = DateTime.Today.ToApiFormattedString(),
                deviceSession = new DeviceSession
                {
                    deviceId = session.DeviceId,
                    sessionId = session.SessionId,
                },
                language = appSettings.DefaultCulture
            };

            var result = await genericHttpClient.PostAsync<BusLocationsResponse, BaseRequest>(obiletOptions.ClientName, obiletOptions.BuslocationsUrl, request);

            return result!;
        }
    }
}
