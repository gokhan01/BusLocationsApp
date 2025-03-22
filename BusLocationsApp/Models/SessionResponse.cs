namespace BusLocationsApp.Models
{
    public class SessionResponse
    {
        public string status { get; set; }
        public Data data { get; set; }
        public object message { get; set; }
        public object userMessage { get; set; }
        public object apiRequestId { get; set; }
        public string controller { get; set; }
        public object clientRequestId { get; set; }
        public object webCorrelationId { get; set; }
        public string correlationId { get; set; }
        public object parameters { get; set; }
    }

    public class Data : DeviceSession
    {
        public object affiliate { get; set; }
        //[JsonPropertyName("device-type")]
        public int deviceType { get; set; }
        public object device { get; set; }
        //[JsonPropertyName("ip-country")]
        public string ipCountry { get; set; }
        //[JsonPropertyName("clean-session-id")]
        public int cleanSessionId { get; set; }
        //[JsonPropertyName("clean-device-id")]
        public int cleanDeviceId { get; set; }
        public object ipAddress { get; set; }
    }
}
