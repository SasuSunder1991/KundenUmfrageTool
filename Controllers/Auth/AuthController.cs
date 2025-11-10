using KundenUmfrageTool.Api.Data;
using KundenUmfrageTool.Api.Dtos;
using KundenUmfrageTool.Api.Models;
using KundenUmfrageTool.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KundenUmfrageTool.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        // AppDBContext => Verbindung zur Datenbank (über EF Core)
        private readonly AppDbContext _db;

        // Hilfsdienst=> Beim login JWT-Tokin erstellt.Diese Token brauchst du später ,damit sich Benutzer anmelden können
        private readonly TokenService _token;


        // Konstruktor → Dependency Injection: DB & TokenService werden automatisch bereitgestellt
        public AuthController(AppDbContext db, TokenService token)
        {
            _db = db;
            _token = token;
        }

        // -------------------- REGISTER --------------------

        // Diese Method reagiert aug einen HTTP POST(also wenn jemand Daten Sendet)
        //"register" hängt sich an deine Route dran => /api/Auth/register
        // POST /api/Auth/register
        [HttpPost("register")]
        [AllowAnonymous] // ✅ wichtig – darf ohne Token aufgerufen werden
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var exists = await _db.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (exists is not null)
                return BadRequest("Email already registered.");

            var hash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var user = new User
            {
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                RoleId = dto.RoleId, // z. B. 1 = Admin, 2 = Manager
                PasswordHash = hash,
                IsActive = true
            };

            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();

            return Ok("Registered successfully.");
        }


        // -------------------- LOGIN --------------------

        [HttpPost("login")]
        [AllowAnonymous] // sonst kommt 401, wenn man noch keinen Token hat
        public async Task<ActionResult<AuthResponse>> Login([FromBody] LoginDto dto)
        {
            // 1 Benutzer + Rolle laden
            var user = await _db.Users
                .Include(u => u.Role) // 👈 Rolle mitladen! sonst bleibt user.Role null
                .FirstOrDefaultAsync(u => u.Email == dto.Email);

            if (user is null)
                return Unauthorized("Invalid credentials.");

            // 2️ Passwort prüfen
            if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                return Unauthorized("Invalid credentials.");

            // 3 ggf. deaktivierte User abweisen
            if (!user.IsActive)
                return Unauthorized("User disabled.");

            // 4️ Token erstellen
            var token = _token.CreateToken(user);

            // 5️ Token zurückgeben
            return Ok(new AuthResponse(token));
        }
    }
}
