using BusLocationsApp.Models;
using BusLocationsApp.Models.Configuration;
using BusLocationsApp.Services.Interfaces;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace BusLocationsApp.Services.Concretes
{
    //Primary Constructor
    public class OBiletSessionClient(IHttpClientFactory httpClientFactory, IOptions<OBiletDistribusionApiOptions> options) : IOBiletSessionClient
    {
        private readonly OBiletDistribusionApiOptions obiletOptions = options.Value;

        /// <summary>
        /// Returns OBilet Distribusion API's session data.
        /// </summary>
        public async Task<SessionResponse> GetSessionAsync(SessionRequest request)
        {
            HttpClient httpClient = httpClientFactory.CreateClient(obiletOptions.ClientName);

            var serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.KebabCaseLower // Makes all properties kebab-case
            };

            var stringContent = new StringContent(JsonSerializer.Serialize(request, serializerOptions), Encoding.UTF8, Application.Json);

            var response = await httpClient.PostAsync(obiletOptions.SessionUrl, stringContent);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();

                var sessionResponse = JsonSerializer.Deserialize<SessionResponse>(jsonResponse, serializerOptions);

                if (sessionResponse == null)
                {
                    throw new ArgumentNullException(nameof(sessionResponse), "Session API returned no result.");
                }

                return sessionResponse;
            }

            throw new Exception($"Session API request failed! HTTP {response.StatusCode}");
        }
    }
}
