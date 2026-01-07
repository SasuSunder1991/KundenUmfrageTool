using KundenUmfrageTool.Api.Dtos.Users;

namespace KundenUmfrageTool.Api.Services
{
    public interface IUserService
    {
        // BenutzerList laden
        Task<IEnumerable<UserListDto>> GetUsersAsync(bool? isActive);
        // Einzelner Benutzer Laden(Detail/ Bearbeiten)
        Task<UserDetailDto?> GetUserAsync(int id);
        // Benutzer anlegen
        Task<UserDetailDto> CreateUserAsync(CreateUserDto dto);
        //Benutzer bearbeiten
        Task<UserDetailDto?> UpdateUserAsync(int id, UpdateUserDto dto);
        // Benutzer aktiv / anaktiv setzen 
        Task<bool> SetActiveAsync(int id, bool active);

    }
}
/*
 * Task<IEnumerable<UserListDto>> GetUsersAsyn(bool? isActive);
was machst das ??
Gibt mehrere Benutzer List 
Als Liste 
Für die Benutzerübersicht 
Warum Task ?
Datenbankzugriff -> asynchron
blockiert den Server nicht
Warum Bool? isActive
bool? = nullable isActive

 


Task<UserDetailDto?> GetUserAsync(int id);
Warum?
für BearbeitungsFormular
Benutzer per ID laden
Warum Userdetaildto?
Enthält FirstName , LastName , RoleId, IsActive
Genau das, was ein Formulla braucht

Warum ? nullable?
Benutzer kann nicht existieren
dann gibt die Methode null zurück
Controller kann sauber NotFound liefern

Task<UserDetailDto?> CreateUserAsync(CreateUserDto dto);
Warum ?
QM -Manager legt neuen Benutzer an
Warum CraeteUserDto?
Erthält nur:
FirstName
LastName 
Email
RoleId
Password
Sicher(Keine ID , Keine IsActive vom Frontend)
Ruckgabe 
Der neu erstellete Benutzer 
Als UserDetailDto


Task<UserDetailDto?> UpdateUserAsync(int id, UpdateUserDto dto);
Warum ? 
Bestenhenden Benutzer ändern
Email & Passwort ändern
Email und Passwort bleiben unverändert
Warum Zwei Parameter?
id=> Aus URL(/users/5)
dto=> neue Daten aus Body
Warum Rückgabe UserDetailDto?
null, wenn benutzer nicht existiert
sonst aktualisierte Daten

Task<bool> SetActiveAsync(int id, bool active);
Warum?
Soft Delete
Benutzer nicht löschen , nur deaktivieren
Rückgabe bool:
true => erfolgerich
false => Benutzer nicht gefunden 


Warum brauchen wir dieses Interface ÜBERHAUPT?
❌ Ohne Interface:
public UsersController(UserService service)


Probleme:

stark gekoppelt

schwer testbar

schlechter Stil

 Mit Interface:
public UsersController(IUserService service)


Vorteile:

lose Kopplung

testbar (Mock)

professionell

IHK-konform

Standard in ASP.NET Core
 */