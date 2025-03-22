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
        public HomeController(ILogger<HomeController> logger, IOBiletSessionProvider oBiletSessionProvider)
        {
            _logger = logger;
            _oBiletSessionProvider = oBiletSessionProvider;
        }

        public async Task<IActionResult> Index()
        {
            var (sessionId, deviceId) = await _oBiletSessionProvider.GetOrCreateSessionAsync();


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
