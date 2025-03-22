using BusLocationsApp.Helpers;
using BusLocationsApp.Models;
using BusLocationsApp.Models.Configuration;
using BusLocationsApp.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace BusLocationsApp.Services.Concretes
{
    public class OBiletBusLocationsClient(GenericHttpClient genericHttpClient, IOBiletSessionProvider oBiletSessionProvider, IOptions<OBiletDistribusionApiOptions> options) : IOBiletBusLocationsClient
    {
        private readonly OBiletDistribusionApiOptions obiletOptions = options.Value;

        public async Task<BusLocationsResponse> GetLocations(string? search = null)
        {
            var session = await oBiletSessionProvider.GetOrCreateSessionAsync();

            //todo: baserequest için ortak model yaratma uygulanabilir.
            BaseRequest request = new()
            {
                data = search,
                date = DateTime.Today.ToString("yyyy-MM-dd"),//ekrandan mı alınacak incelenecek
                deviceSession = new DeviceSession
                {
                    deviceId = session.DeviceId,
                    sessionId = session.SessionId,
                },
                language = "tr-TR"
            };

            var result = await genericHttpClient.PostAsync<BusLocationsResponse, BaseRequest>(obiletOptions.ClientName, obiletOptions.BuslocationsUrl, request);

            return result!;
        }
    }
}
