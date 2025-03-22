using BusLocationsApp.Helpers;
using BusLocationsApp.Models;
using BusLocationsApp.Models.Configuration;
using BusLocationsApp.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace BusLocationsApp.Services.Concretes
{
    //Primary Constructor
    public class OBiletSessionClient(GenericHttpClient genericHttpClient, IOptions<OBiletDistribusionApiOptions> options) : IOBiletSessionClient
    {
        private readonly OBiletDistribusionApiOptions obiletOptions = options.Value;

        /// <summary>
        /// Returns OBilet Distribusion API's session data.
        /// </summary>
        public async Task<SessionResponse> GetSessionAsync(SessionRequest request)
        {
            var result = await genericHttpClient.PostAsync<SessionResponse, SessionRequest>(obiletOptions.ClientName, obiletOptions.SessionUrl, request);

            return result!;
        }
    }
}
