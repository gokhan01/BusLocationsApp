using BusLocationsApp.Services.Interfaces;

namespace BusLocationsApp.Services.Concretes
{
    public class OBiletSessionProvider(IOBiletSessionManager oBiletSessionManager, IOBiletSessionClient oBiletSessionClient) : IOBiletSessionProvider
    {
        public async Task<(string SessionId, string DeviceId)> GetOrCreateSessionAsync()
        {
            var (sessionId, deviceId) = oBiletSessionManager.GetSession();

            if (string.IsNullOrEmpty(sessionId) || string.IsNullOrEmpty(deviceId))
            {
                var newSession = await oBiletSessionClient.GetSessionAsync(new Models.SessionRequest
                {
                    //todo:tarayıcı bilgileri httpcontext den alınabilir
                    type = 7,
                    application = new Models.ApplicationModel
                    {
                        EquipmentId = "distribusion",
                        version = "1.0.0.0"
                    },
                    browser = new Models.Browser
                    {
                        version = "1.0.0.0",
                        name = "Chrome"
                    },
                    connection = new Models.Connection
                    {
                        IpAddress = "165.114.41.21",
                        port = "8080"
                    }
                });

                if (newSession != null)
                {
                    sessionId = newSession.data.sessionId;
                    deviceId = newSession.data.deviceId;

                    oBiletSessionManager.SaveSession(sessionId, deviceId);
                }
            }

            return (sessionId!, deviceId!);
        }
    }
}
