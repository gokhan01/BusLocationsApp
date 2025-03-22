using System.Text;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace BusLocationsApp.Helpers
{
    public class GenericHttpClient(IHttpClientFactory httpClientFactory)
    {
        /// <summary>
        /// Sends a POST request to the specified endpoint with the given payload and returns the response as a generic type.
        /// </summary>
        /// <typeparam name="TResponse">The type of the expected response.</typeparam>
        /// <typeparam name="TRequest">The type of the request payload.</typeparam>
        /// <param name="clientName">The name of the HttpClient to use (registered with IHttpClientFactory).</param>
        /// <param name="endpoint">The endpoint to send the POST request to.</param>
        /// <param name="payload">The request payload.</param>
        /// <returns>The response deserialized as the specified type.</returns>
        public async Task<TResponse?> PostAsync<TResponse, TRequest>(string clientName, string endpoint, TRequest payload)
        {
            var httpClient = httpClientFactory.CreateClient(clientName);

            var serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.KebabCaseLower // Makes all properties kebab-case
            };

            var stringContent = new StringContent(JsonSerializer.Serialize(payload, serializerOptions), Encoding.UTF8, Application.Json);

            var response = await httpClient.PostAsync(endpoint, stringContent);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();

                var deserialized = JsonSerializer.Deserialize<TResponse>(jsonResponse, serializerOptions);

                if (deserialized == null)
                {
                    throw new ArgumentNullException(nameof(deserialized), "Session API returned no result.");
                }

                return deserialized;
            }

            throw new Exception($"Session API request failed! HTTP {response.StatusCode}");
        }
    }
}
