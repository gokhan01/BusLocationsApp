using BusLocationsApp.Services.Interfaces;

namespace BusLocationsApp.Services.Concretes
{
    /// <summary>
    /// Provides functionality to manage and retrieve sessions.
    /// Combines session retrieval logic and storage to ensure
    /// consistent and reusable session management.
    /// </summary>
    public class OBiletSessionProvider(IOBiletSessionManager oBiletSessionManager, IOBiletSessionClient oBiletSessionClient) : IOBiletSessionProvider
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
                // Fetch a new session from the session API.
                var newSession = await oBiletSessionClient.GetSessionAsync(new Models.SessionRequest
                {
                    //todo:tarayıcı bilgileri httpcontext den alınabilir
                    type = 7,
                    application = new Models.ApplicationModel
                    {
                        equipmentId = "distribusion",
                        version = "1.0.0.0"
                    },
                    browser = new Models.Browser
                    {
                        version = "1.0.0.0",
                        name = "Chrome"
                    },
                    connection = new Models.Connection
                    {
                        ipAddress = "165.114.41.21",
                        port = "8080"
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
