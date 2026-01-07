using KundenUmfrageTool.Api.Dtos;
using KundenUmfrageTool.Api.Dtos.Users;
using KundenUmfrageTool.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/users")]
[Authorize(Policy = "QM")]  // nur QM darf User verwalten
public class UsersController : ControllerBase
{
    private readonly IUserService _service;

    public UsersController(IUserService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserListDto>>> GetUsers([FromQuery] bool? isActive)
        => Ok(await _service.GetUsersAsync(isActive));

    [HttpGet("{id:int}")]
    public async Task<ActionResult<UserDetailDto>> GetUser(int id)
    {
        var result = await _service.GetUserAsync(id);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<UserDetailDto>> CreateUser([FromBody] CreateUserDto dto)
    {
        var user = await _service.CreateUserAsync(dto);
        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<UserDetailDto>> UpdateUser(int id, [FromBody] UpdateUserDto dto)
    {
        var updated = await _service.UpdateUserAsync(id, dto);
        return updated == null ? NotFound() : Ok(updated);
    }

    [HttpPatch("{id:int}/status")]
    public async Task<IActionResult> SetStatus(int id, [FromBody] bool status)
    {
        var ok = await _service.SetActiveAsync(id, status);
        return ok ? NoContent() : NotFound();
    }
}

/*
 * WAS IST DER UsersController?

 Der UsersController ist die HTTP-Schnittstelle für:

Benutzer anzeigen

Benutzer anlegen

Benutzer bearbeiten

Benutzer aktiv / inaktiv setzen

Der Controller enthält KEINE Business-Logik
➡ Er ruft nur den UserService auf



[ApiController]
[Route("api/users")]
[Authorize(Policy = "QM")]
public class UsersController : ControllerBase
[ApiController]
✔ Aktiviert:

automatische Model-Validierung

saubere Fehler (400 bei falschem Body)

private readonly IUserService _service;

public UsersController(IUserService service)
{
    _service = service;
}
Erklärung:
Controller kennt nur das Interface

Keine DB

Kein EF Core

Kein Mapping

✔ lose Kopplung
✔ testbar
✔ Best Practice


 */