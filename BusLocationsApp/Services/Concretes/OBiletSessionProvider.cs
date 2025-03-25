using BusLocationsApp.Helpers.Utilities;
using BusLocationsApp.Services.Interfaces;
using UAParser;
namespace BusLocationsApp.Services.Concretes
{
    /// <summary>
    /// All requests made to obilet.com business API should be coded in the MVC application backend 
    /// no client side requests should be made directly to obilet.com business api, 
    /// any client side requests implemented should be made to the application backend. 
    /// </summary>
    public class OBiletSessionProvider(IOBiletSessionManager oBiletSessionManager, IOBiletSessionClient oBiletSessionClient, IBrowserInfoProvider browserInfoProvider, IClientInfoProvider clientInfoProvider) : IOBiletSessionProvider
    {
        /// <summary>
        /// Retrieves an existing session or creates a new one if none is available.
        /// </summary>
        /// <returns>
        /// A tuple containing the session ID and device ID. 
        /// If a session doesn't exist, a new session is fetched from the OBilet session API and stored locally.
        /// </returns>
        /// <exception cref="InvalidOperationException">Thrown if a new session cannot be fetched from the API.</exception>
        public async Task<(string SessionId, string DeviceId)> GetOrCreateSessionAsync()
        {
            // Retrieve the current session from the session manager.
            var (sessionId, deviceId) = oBiletSessionManager.GetSession();

            //Naming the if condition according to its purpose to increase readability.
            if (CheckSessionData(sessionId, deviceId))
            {
                var (browserName, browserVersion) = browserInfoProvider.GetBrowserInfo();
                var (ipAddress, port) = clientInfoProvider.GetClientInfo();

                // Fetch a new session from the session API.
                var newSession = await oBiletSessionClient.GetSessionAsync(new Models.SessionRequest
                {
                    type = 7,
                    application = new Models.ApplicationModel
                    {
                        equipmentId = "distribusion",
                        version = "1.0.0.0"
                    },
                    browser = new Models.Browser
                    {
                        version = browserVersion,
                        name = browserName
                    },
                    connection = new Models.Connection
                    {
                        ipAddress = ipAddress,
                        port = port.ToString()
                    }
                });

                if (newSession != null)
                {
                    // Update the session and device IDs with the new data.
                    sessionId = newSession.data.sessionId;
                    deviceId = newSession.data.deviceId;

                    // Save the new session data locally for future use.
                    oBiletSessionManager.SaveSession(sessionId, deviceId);
                }
                else
                {
                    // Handle cases where the API fails to return a valid session.
                    throw new InvalidOperationException("Unable to fetch a new session from the API.");
                }
            }

            // Return the existing or newly created session data.
            return (sessionId!, deviceId!);
        }

        private static bool CheckSessionData(string? sessionId, string? deviceId)
        {
            return string.IsNullOrEmpty(sessionId) || string.IsNullOrEmpty(deviceId);
        }
    }
}
