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
        public HomeController(ILogger<HomeController> logger, IOBiletSessionProvider oBiletSessionProvider, IOBiletBusLocationsClient oBiletBusLocationsClient)
        {
            _logger = logger;
            _oBiletSessionProvider = oBiletSessionProvider;
            _oBiletBusLocationsClient = oBiletBusLocationsClient;
        }

        public async Task<IActionResult> Index()
        {
            //var (sessionId, deviceId) = await _oBiletSessionProvider.GetOrCreateSessionAsync();

            var res = await _oBiletBusLocationsClient.GetLocations();

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
