using KundenUmfrageTool.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KundenUmfrageTool.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // Erlaubt sind nur Benutzer mit Admin-Rolle => Nur wer beim Login ein JWT-Token mir "role:Admin" bekommt kann diese Funktionen aufrufen
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly AdminService _service;
        public AdminController(AdminService service) => _service = service;

        //Admin ruft Liste alle Benutzer ab evtl. mit Suchfilter(?search=Anna)
        //  [FromQuery] bedeutet: Der Parameter search wird aus der URL-Abfrage (Query-String) entnommen.
        [HttpGet("users")]
        public async Task<IActionResult> GetUsers([FromQuery] string? search = null)
            => Ok(await _service.GetUserListAsync(search));

        // Admin schaut sich Detaildaten eine Benutzer an(z.B. Name, Email, Rolle, Status)
        [HttpGet("users/{id:int}")]
        public async Task<IActionResult> GetUserDetail(int id)
        {
            var dto = await _service.GetUserDetailAsync(id);
            return dto is null ? NotFound("User not found") : Ok(dto);
        }
    }
}
