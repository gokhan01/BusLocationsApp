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
            if (destinationId == originId)
            {
                TempData["errorMessage"] = "Başlangıç ​​ve varış noktası aynı olamaz!";
                return RedirectToAction("Index", "Home");
            }

            DateTime _departureDate = Convert.ToDateTime(departureDate, new CultureInfo("tr-TR"));

            if (_departureDate < DateTime.Today)
            {
                TempData["errorMessage"] = "Kalkış tarihi için geçerli minimum tarih bugün'dür!";
                return RedirectToAction("Index", "Home");
            }

            var journeys = await _oBiletJourneysClient.GetJourneys(new JourneysRequest
            {
                departureDate = _departureDate.ToString("yyyy-MM-dd"),
                destinationId = destinationId,
                originId = originId
            });

            var viewModel = journeys.data.OrderBy(d => d.journey.departure).Select(x => new JourneysViewModel
            {
                Arrival = x.journey.arrival.ToString("HH:mm"),
                Departure = x.journey.departure.ToString("HH:mm"),
                Destination = x.journey.destination,
                Origin = x.journey.origin,
                OriginalPrice = x.journey.originalPrice.ToString("0.00"),
                Currency = x.journey.currency
            });

            ViewBag.OriginLocation = journeys.data.FirstOrDefault()?.originLocation;
            ViewBag.DestinationLocation = journeys.data.FirstOrDefault()?.destinationLocation;
            ViewBag.DepartureDate = _departureDate.ToString("d MMMM yyyy", new CultureInfo("tr-TR"));

            //data.journey.departure
            //data.journey.arrival
            //data.journey.origin
            //data.journey.destination
            //data.journey.original-price

            return View(viewModel);
        }
    }
}
