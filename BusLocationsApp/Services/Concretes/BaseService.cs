using BusLocationsApp.Helpers.Extensions;
using BusLocationsApp.Models.Configuration;
using BusLocationsApp.Models;
using BusLocationsApp.Services.Interfaces;

namespace BusLocationsApp.Services.Concretes
{
    public abstract class BaseService
    {
        protected readonly IOBiletSessionProvider _oBiletSessionProvider;
        protected readonly AppSettings AppSettings;

        protected BaseService(IOBiletSessionProvider oBiletSessionProvider, AppSettings appSettings)
        {
            _oBiletSessionProvider = oBiletSessionProvider;
            AppSettings = appSettings;
        }

        protected async Task<BaseRequest> CreateBaseRequestAsync<T>(T data)
        {
            var (SessionId, DeviceId) = await _oBiletSessionProvider.GetOrCreateSessionAsync();

            return new BaseRequest
            {
                data = data,
                date = DateTime.Today.ToApiFormattedString(),
                deviceSession = new DeviceSession
                {
                    deviceId = DeviceId,
                    sessionId = SessionId,
                },
                language = AppSettings.DefaultCulture
            };
        }
    }
}
