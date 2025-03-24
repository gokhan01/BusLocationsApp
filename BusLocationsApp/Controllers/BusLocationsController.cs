using BusLocationsApp.Models;
using BusLocationsApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BusLocationsApp.Controllers
{
    public class BusLocationsController : Controller
    {
        private readonly IOBiletBusLocationsClient _oBiletBusLocationsClient;

        public BusLocationsController(IOBiletBusLocationsClient oBiletBusLocationsClient)
        {
            _oBiletBusLocationsClient = oBiletBusLocationsClient;
        }

        //Users should be able to perform text-based search on origin and destination fields. The search keyword user
        //enters should be used in order to fetch related bus locations from the obilet.com business API
        //GetBusLocations method.
        public async Task<IActionResult> Get(string? term = null)
        {
            var result = await _oBiletBusLocationsClient.GetLocationsAsync(term);

            var data = result.data.Select(d => new Select2ViewModel
            {
                id = d.id,
                text = d.name
            });

            return Json(data);
        }
    }
}
