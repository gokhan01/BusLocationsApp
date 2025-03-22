namespace BusLocationsApp.Models
{
    public class BaseRequest
    {
        public string? data { get; set; }
        public DeviceSession deviceSession { get; set; }
        public DateTime date { get; set; }
        public string language { get; set; }
    }

    public class DeviceSession
    {
        public string sessionId { get; set; }
        public string deviceId { get; set; }
    }
}
