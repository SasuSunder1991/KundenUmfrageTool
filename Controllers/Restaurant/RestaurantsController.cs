using KundenUmfrageTool.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KundenUmfrageTool.Api.Controllers.Restaurant
{
    // Controller für alle Restaurant-bezogenen API-Endpunkte
    [ApiController]
    [Route("api/restaurants")]
    public class RestaurantsController : ControllerBase
    {
        private readonly IRestaurantService _service;

        // Service per Dependency Injection
        public RestaurantsController(IRestaurantService service)
        {
            _service = service;
        }

        // --------------------------------------------------------------------
        // GET /api/restaurants
        // QM-Manager: sieht ALLE Restaurants
        // Policy „QM“ wird serverseitig geprüft
        // --------------------------------------------------------------------
        [HttpGet]
        [Authorize(Policy = "QM")]
        public async Task<IActionResult> GetAll()
        {
            var list = await _service.GetAllAsync();
            return Ok(list);
        }
        // --------------------------------------------------------------------
        // GET /api/restaurants/mine
        // Restaurant-Manager: sieht NUR sein eigenes Restaurant
        // Claim “NameIdentifier” enthält die UserId aus dem JWT-Token
        // --------------------------------------------------------------------

        [HttpGet("mine")]
        [Authorize(Policy = "RestaurantManager")]
        public async Task<IActionResult> GetMine()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
                return Forbid();

            var userId = int.Parse(userIdClaim);

            var list = await _service.GetForManagerAsync(userId);
            return Ok(list);
        }

        // --------------------------------------------------------------------
        // POST /api/restaurants
        // QM-Manager darf Restaurants anlegen
        // RestaurantDto enthält Formulardaten aus dem Frontend
        // --------------------------------------------------------------------
        [HttpPost]
        [Authorize(Policy = "QM")]
        public async Task<IActionResult> Create([FromBody] RestaurantDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return Ok(created);
        }

        // --------------------------------------------------------------------
        // PUT /api/restaurants/{id}
        // QM-Manager darf Restaurant-Daten ändern
        // --------------------------------------------------------------------
        [HttpPut("{id}")]
        [Authorize(Policy = "QM")]
        public async Task<IActionResult> Update(int id, [FromBody] RestaurantDto dto)
        {
            await _service.UpdateAsync(id, dto);
            return NoContent();
        }

        // --------------------------------------------------------------------
        // DELETE /api/restaurants/{id}
        // QM-Manager darf Restaurant löschen
        // --------------------------------------------------------------------
        [HttpDelete("{id}")]
        [Authorize(Policy = "QM")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
