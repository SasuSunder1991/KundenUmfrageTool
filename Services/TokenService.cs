using KundenUmfrageTool.Api.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace KundenUmfrageTool.Api.Services
{
    public class TokenService
    {
        // Der Service bekommt die App-Konfiguration(appsetting.json), damit er die geheimen Einstellungen (wie den Schlüssel und Ablaufzeit) lesen kann.
        private readonly IConfiguration _config;
        public TokenService(IConfiguration config) => _config = config;

        // Die Methode erstellt das token für einen bestimmten Benutzer
        public string CreateToken(User user)
        {
            //Claim= kleine Info über den Benutzer, die im Token steht
            // wird gespeichet: 
            //NameIdentifier → Benutzer-ID     Email → Benutzer-E-Mail  Role → z. B. "Admin" oder "Manager"
            // Wenn später jemand mit einem Token anfragt, kann das System daraus lesen, wer das ist und welche Rechte er hat.
            var claims = new List<Claim>
            {
                // Benutzer-ID
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),

                // E-Mail
                new Claim(ClaimTypes.Email, user.Email),

                // Rollenname (optional, aber lassen wir drin)
                new Claim(ClaimTypes.Role, user.Role?.Name ?? string.Empty),

                // WICHTIG! → Rollensystem  App
                // QM = 1, RestaurantManager = 2
                new Claim("roleId", user.RoleId.ToString())
            };

            // das ist geheime Schlüssel, mit dem Token verschlüsselt und signiert wird.
            // Nur das Backend kennt diesen Schlüssel → niemand anderes kann ein gültiges Token fälschen.

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));


            //Damit wird das Token mit dem geheimen Schlüssel und dem Algorithmus HmacSha256 digital unterschrieben.
            //So kann das System später prüfen, ob das Token echt ist.
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //Hier wird festgelegt, wie lange das Token gültig bleibt, z. B. 2 Stunden.
            //Danach muss sich der Benutzer neu einloggen, um ein neues Token zu bekommen.
            var hours = int.TryParse(_config["Jwt:ExpiresHours"], out var h) ? h : 2;

            var token = new JwtSecurityToken(
                //Issuer → Wer das Token erstellt (z. B. dein Backend)
                // Audience → Wer das Token benutzen darf (z. B. Frontend)
                // Claims → Benutzerinformationen
                //Expires → Ablaufzeit
                issuer: _config["Jwt:Issuer"], audience: _config["Jwt:Audience"],
                claims: claims, expires: DateTime.UtcNow.AddHours(hours),
                signingCredentials: creds);

            //Wandelt das Token-Objekt in einen String um, Dieser String wird dann an den Benutzer geschickt (z. B. als Login-Antwort).
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}


/*
 Der Service erstellt login-Token
Das steht direkt im Pflichtenheft (RBAC / Token-System)
Was macht das Token?

Ein Token speichert:

 * Benutzer-ID

 * E-Mail

* Rolle (QM, RM)

* roleId (1 = QM, 2 = Manager)

* Ablaufzeit (z. B. 2h)


Wozu?

Das Token erlaubt dem Backend:

* zu prüfen WER anfragt

* zu prüfen WELCHE Rechte die Person hat

* zu verhindern, dass Manager fremde Restaurants sehen

* das Frontend kann basierend auf Rolle das Menü filtern



TokenService = Login-Herzstück.
Er erstellt den Berechtigungs-Ausweis für das Frontend.

 
 
 
 
 
 
 
 
 
 
 */