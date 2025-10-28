using KundenUmfrageTool.Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KundenUmfrageTool.Api.Controllers.Public
{
    [ApiController]
    [Route("api/public")]
    public class PublicController : ControllerBase
    {
        private readonly AppDbContext _db;

        public PublicController(AppDbContext db)
        {
            _db = db;
        }

        // --- 1️⃣ Alle Checkpoints ---
        [HttpGet("checkpoints")]
        public async Task<IActionResult> GetCheckpoints()
        {
            var checkpoints = await _db.Checkpoints.ToListAsync();
            return Ok(checkpoints);
        }

        // --- 2️⃣ Alle Restaurants ---
        [HttpGet("restaurants")]
        public async Task<IActionResult> GetRestaurants()
        {
            var restaurants = await _db.Restaurants
                .Include(r => r.ManagerUser)  // falls Navigation-Property existiert
                .ToListAsync();

            return Ok(restaurants);
        }
    }
}
