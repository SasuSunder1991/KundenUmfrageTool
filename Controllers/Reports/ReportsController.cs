using KundenUmfrageTool.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KundenUmfrageTool.Api.Controllers.Reports
{
    [ApiController]
    [Route("api/reports")]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _service;

        public ReportsController(IReportService service)
        {
            _service = service;
        }

        // Restaurant-Manager: nur eigenes Restaurant auswerten
        [HttpGet("{restaurantId}")]
        [Authorize(Policy = "RestaurantManager")]
        public async Task<IActionResult> GetReport(int restaurantId)
        {
            var result = await _service.GetReportForRestaurant(restaurantId);
            return Ok(result);
        }
    }
}

