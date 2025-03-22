using System.Text.Json.Serialization;

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
        public object clientrequestid { get; set; }
        public object webcorrelationid { get; set; }
        public string correlationid { get; set; }
        public object parameters { get; set; }
    }

    public class Data
    {
        //[JsonPropertyName("session-id")]
        public string sessionId { get; set; }
        //[JsonPropertyName("device-id")]
        public string deviceId { get; set; }
        public object affiliate { get; set; }
        //[JsonPropertyName("device-type")]
        public int devicetype { get; set; }
        public object device { get; set; }
        //[JsonPropertyName("ip-country")]
        public string ipcountry { get; set; }
        //[JsonPropertyName("clean-session-id")]
        public int cleansessionid { get; set; }
        //[JsonPropertyName("clean-device-id")]
        public int cleandeviceid { get; set; }
        public object ipaddress { get; set; }
    }
}
