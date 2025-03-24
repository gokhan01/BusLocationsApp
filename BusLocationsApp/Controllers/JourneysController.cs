using BusLocationsApp.Helpers.Extensions;
using BusLocationsApp.Models;
using BusLocationsApp.Models.Configuration;
using BusLocationsApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Globalization;

namespace BusLocationsApp.Controllers
{
    public class JourneysController : Controller
    {
        private readonly IOBiletJourneysClient _oBiletJourneysClient;
        private readonly AppSettings appSettings;

        public JourneysController(IOBiletJourneysClient oBiletJourneysClient, IOptions<AppSettings> settings)
        {
            _oBiletJourneysClient = oBiletJourneysClient;
            appSettings = settings.Value;
        }

        //The origin, destination and departure date provided by the user should be passed to the obilet.com business API GetJourneys method.
        public async Task<IActionResult> IndexAsync(string departureDate, int destinationId, int originId)
        {
            //Users can not select same location as both origin and destination. BE validation.
            if (destinationId == originId)
            {
                TempData["errorMessage"] = "Başlangıç ​​ve varış noktası aynı olamaz!";
                return RedirectToAction("Index", "Home");
            }

            DateTime _departureDate = Convert.ToDateTime(departureDate, new CultureInfo(appSettings.DefaultCulture));

            //Minimum valid date for departure date is Today. BE validation.
            if (_departureDate < DateTime.Today)
            {
                TempData["errorMessage"] = "Kalkış tarihi için geçerli minimum tarih bugün'dür!";
                return RedirectToAction("Index", "Home");
            }

            var journeys = await _oBiletJourneysClient.GetJourneys(new JourneysRequest
            {
                departureDate = _departureDate.ToApiFormattedString(),
                destinationId = destinationId,
                originId = originId
            });

            //Journeys returned by the API response should be sorted by their departure times and displayed to the user.
            var viewModel = journeys.data.OrderBy(d => d.journey.departure).Select(x => new JourneysViewModel
            {
                Arrival = x.journey.arrival.ToHourString(),
                Departure = x.journey.departure.ToHourString(),
                Destination = x.journey.destination,
                Origin = x.journey.origin,
                OriginalPrice = x.journey.originalPrice.ToString("0.00"),
                Currency = x.journey.currency
            });

            ViewBag.OriginLocation = journeys.data.FirstOrDefault()?.originLocation;
            ViewBag.DestinationLocation = journeys.data.FirstOrDefault()?.destinationLocation;
            ViewBag.DepartureDate = _departureDate.ToFormattedString("d MMMM yyyy");

            return View(viewModel);
        }
    }
}
