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

        // --- 1️ Alle Checkpoints ---
        [HttpGet("checkpoints")]
        public async Task<IActionResult> GetCheckpoints()
        {
            var checkpoints = await _db.Checkpoints.ToListAsync();
            return Ok(checkpoints);
        }

        // --- 2️ Alle Restaurants ---
        [HttpGet("restaurants")]
        public async Task<IActionResult> GetRestaurants()
        {
            var restaurants = await _db.Restaurants
                .Include(r => r.ManagerUser) // falls vorhanden
                .ToListAsync();

            return Ok(restaurants);
        }
        [HttpGet("surveys")]
        public async Task<IActionResult> GetSurveys()
        {
            var surveys = await _db.Surveys
                .Include(s => s.SurveyCheckpoints)
                    .ThenInclude(sc => sc.Checkpoint)
                .ToListAsync();

            return Ok(surveys);
        }


    }
}
