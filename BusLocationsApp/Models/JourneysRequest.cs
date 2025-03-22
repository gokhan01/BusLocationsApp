namespace BusLocationsApp.Models
{
    public class JourneysRequest
    {
        public int originId { get; set; }
        public int destinationId { get; set; }
        public string departureDate { get; set; }
    }
}
