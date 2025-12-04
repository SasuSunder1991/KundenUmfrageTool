using KundenUmfrageTool.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KundenUmfrageTool.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // Erlaubt sind nur Benutzer mit Admin-Rolle => Nur wer beim Login ein JWT-Token mir "role:Admin" bekommt kann diese Funktionen aufrufen
    [Authorize(Policy = "QM")]
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


/*AdminController = Benzuterverwaltung
 
   - Diese Controller nur für Qm- Manger erreichbar
    [Authorize(Policy = "QM")]  => laut pflichheft Nur Qm-Manger dürfen Benutzer sehen und verwalten
    
*  Get/api/admin/users
    -Holt alle Benutzer
    - Optional mit Suchfilter? search= Anna
    - Nutzt AdminService.GetUserListAsync()
    -Qm-Manager sieht komplete Benutzerliste


* Get/api/admin/users{id}
  -Holt die Detaildaten eines Benutzer(Name , Email, rolle, status )
  -Gibt 404 , wenn der Benutzer nicht existert
  -QM-Manager kann Benutzer anklicken und Details sehen.

  Warum gibt es diesen Controller?
  QM soll laut Anforderung Benuzuter 
    

    
 
 
 
 
 */