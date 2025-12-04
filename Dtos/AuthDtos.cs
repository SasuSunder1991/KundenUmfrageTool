namespace KundenUmfrageTool.Api.Dtos
{
    public record RegisterDto(string Email, string Password, string FirstName, string LastName, int RoleId);
    public record LoginDto(string Email, string Password);
    public record AuthResponse(string Token);
}


// record = für kurze, feste Datenobjekte (Requests, Responses)
// class = für größere , flexible Objekt (Benutzerverwaltung)

// Warum record bei meinen Auth-DTOs

//Meine RegisterDto, LoginDto, AuthResponse sind nur einfache Datenträger.
//Sie haben keine Methoden, keine Logik, sondern bestehen nur aus Eingabe- oder Ausgabedaten.
