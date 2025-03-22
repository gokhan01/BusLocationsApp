using System.Text.Json.Serialization;

namespace BusLocationsApp.Models
{
    public class SessionRequest
    {
        public int type { get; set; }
        public Connection connection { get; set; }
        public ApplicationModel application { get; set; }
        public Browser browser { get; set; }
    }

    public class Connection
    {
        //[JsonPropertyName("ip-address")]
        public string IpAddress { get; set; }
        public string port { get; set; }
    }

    public class ApplicationModel
    {
        public string version { get; set; }
        //[JsonPropertyName("equipment-id")]
        public string EquipmentId { get; set; }
    }

    public class Browser
    {
        public string name { get; set; }
        public string version { get; set; }
    }
}
