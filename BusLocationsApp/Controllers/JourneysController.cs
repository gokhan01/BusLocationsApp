using BusLocationsApp.Models;
using BusLocationsApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace BusLocationsApp.Controllers
{
    public class JourneysController : Controller
    {
        private readonly IOBiletJourneysClient _oBiletJourneysClient;

        public JourneysController(IOBiletJourneysClient oBiletJourneysClient)
        {
            _oBiletJourneysClient = oBiletJourneysClient;
        }

        public async Task<IActionResult> IndexAsync(string departureDate, int destinationId, int originId)
        {
            DateTime _departureDate = Convert.ToDateTime(departureDate, new CultureInfo("tr-TR"));

            var journeys = await _oBiletJourneysClient.GetJourneys(new JourneysRequest
            {
                departureDate = _departureDate.ToString("yyyy-MM-dd"),
                destinationId = destinationId,
                originId = originId
            });

            var viewModel = journeys.data.Select(x => new JourneysViewModel
            {
                Arrival = x.journey.arrival.ToString("HH:mm"),
                Departure = x.journey.departure.ToString("HH:mm"),
                Destination = x.journey.destination,
                Origin = x.journey.origin,
                OriginalPrice = x.journey.originalPrice.ToString("0.00"),
                Currency = x.journey.currency
            });

            //data.journey.departure
            //data.journey.arrival
            //data.journey.origin
            //data.journey.destination
            //data.journey.original-price

            return View(viewModel);
        }
    }
}
