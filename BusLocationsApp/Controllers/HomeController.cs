using System.Diagnostics;
using System.Threading.Tasks;
using BusLocationsApp.Models;
using BusLocationsApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BusLocationsApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IOBiletSessionProvider _oBiletSessionProvider;
        private readonly IOBiletBusLocationsClient _oBiletBusLocationsClient;
        private readonly IOBiletJourneysClient _oBiletJourneysClient;
        public HomeController(ILogger<HomeController> logger, IOBiletSessionProvider oBiletSessionProvider, IOBiletBusLocationsClient oBiletBusLocationsClient, IOBiletJourneysClient oBiletJourneysClient)
        {
            _logger = logger;
            _oBiletSessionProvider = oBiletSessionProvider;
            _oBiletBusLocationsClient = oBiletBusLocationsClient;
            _oBiletJourneysClient = oBiletJourneysClient;
        }

        public IActionResult Index()
        {
            //var (sessionId, deviceId) = await _oBiletSessionProvider.GetOrCreateSessionAsync();

            //var res = await _oBiletBusLocationsClient.GetLocations();

            //var journeys = await _oBiletJourneysClient.GetJourneys(new JourneysRequest
            //{
            //    departureDate = DateTime.Today.ToString("yyyy-MM-dd"),
            //    destinationId = 356,
            //    originId = 349
            //});

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
