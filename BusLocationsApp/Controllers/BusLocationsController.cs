using BusLocationsApp.Models;
using BusLocationsApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BusLocationsApp.Controllers
{
    public class BusLocationsController : Controller
    {
        private readonly IOBiletBusLocationsClient _oBiletBusLocationsClient;

        public BusLocationsController(IOBiletBusLocationsClient oBiletBusLocationsClient)
        {
            _oBiletBusLocationsClient = oBiletBusLocationsClient;
        }

        public async Task<IActionResult> Get(string? term = null)
        {
            var result = await _oBiletBusLocationsClient.GetLocations(term);

            //Select2ViewModel viewModel = new()
            //{
            //    results = [.. result.data.Select(d => new Result
            //    {
            //        id = d.id,
            //        text = d.name
            //    })],
            //    pagination = new Pagination { more = true }
            //};

            var data = result.data.Select(d => new Select2ViewModel
            {
                id = d.id,
                text = d.name
            });

            return Json(data);
        }
    }
}
