//using KundenUmfrageTool.Api.Data;
//using KundenUmfrageTool.Api.Services;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace KundenUmfrageTool.Api.Controllers.Public
//{
//    [ApiController]
//    [Route("api/public")]
//    public class PublicController : ControllerBase
//    {
//        private readonly AppDbContext _db;
//        private readonly IRestaurantService _restaurantService;

//        public PublicController(AppDbContext db, IRestaurantService restaurantService)
//        {
//            _db = db;
//            _restaurantService = restaurantService;
//        }

//        // -----------------------------
//        // ✔ 1. GET: Alle Checkpoints
//        // -----------------------------
//        [HttpGet("checkpoints")]
//        public async Task<IActionResult> GetCheckpoints()
//        {
//            var checkpoints = await _db.Checkpoints.ToListAsync();
//            return Ok(checkpoints);
//        }

//        // -----------------------------
//        // ✔ 2. GET: Alle Restaurants (öffentlich)
//        // -----------------------------
//        //[HttpGet("restaurants")]
//        //public async Task<ActionResult<IEnumerable<RestaurantDto>>> GetRestaurants()
//        //{
//        //    var restaurants = await _restaurantService.GetAllAsync();
//        //    return Ok(restaurants);
//        //}

//        // -----------------------------
//        // ✔ 3. GET: Alle Surveys (öffentlich)
//        // -----------------------------
//        [HttpGet("surveys")]
//        public async Task<IActionResult> GetSurveys()
//        {
//            var surveys = await _db.Surveys
//                .Include(s => s.SurveyCheckpoints)
//                .ThenInclude(sc => sc.Checkpoint)
//                .ToListAsync();

//            return Ok(surveys);
//        }

//        // -----------------------------
//        // ✔ 4. GET: Umfrage per ID (optional)
//        // -----------------------------
//        [HttpGet("survey/{id}")]
//        public async Task<IActionResult> GetSurveyById(int id)
//        {
//            var survey = await _db.Surveys
//                .Include(s => s.SurveyCheckpoints)
//                .ThenInclude(sc => sc.Checkpoint)
//                .FirstOrDefaultAsync(s => s.Id == id);

//            if (survey == null)
//                return NotFound();

//            return Ok(survey);
//        }
//    }
//}
