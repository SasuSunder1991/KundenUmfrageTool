using AutoMapper;
using KundenUmfrageTool.Api.Data;
using KundenUmfrageTool.Api.Dtos;
using Microsoft.EntityFrameworkCore;

namespace KundenUmfrageTool.Api.Services
{
    public class AdminService

    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        // Verbindet den Service mit der Datenbank(AppDBContext)
        // Automapper, der automatisch aus Datenmodellen DTOs macht(also Object fürs Frontend)
        public AdminService(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        // Methode GetUserListAsync(string ? search =null)
        // Eine Liste alle Benutzer abrufen -optional gefiltert nach einem Suchbegriff
        public async Task<IEnumerable<UserDto>> GetUserListAsync(string? search = null)
        {

            // Bereritet eine Datenbank-Abfrage für alle Benutzer vor, inklusive ihrer Rolle (Admin, manager etc)
            var q = _db.Users.Include(u => u.Role).AsQueryable();
            // Wenn search angegeben ist (z.B. ?search=admin), filtert die Metthode nach Namen oder Email
            if (!string.IsNullOrWhiteSpace(search))
                q = q.Where(u =>
                    u.Email.Contains(search) ||
                    u.FirstName.Contains(search) ||
                    u.LastName.Contains(search));

            // await q.ToListAsync(); => Führt die Abfrage wirklich in der Datenbank aus
            var users = await q.ToListAsync();
            //wandelt die Datenbank-Objekte in DTOs(vereinfachte Objekte fürs Frontend)um.
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }
        //Methode: GetUserDetailAsync(int id)=> einzelne Benutzerdetails anhands der Id abrufen
        // Include(u =u.Id ==id) ==> auch hier wird die Rolle mitgeladen
        //.FirstOrDefaultAsync(u => u.Id == id) ===> sucht genau den Benutzer mit dieser ID
        // Wenn  gefunden → wird er mit AutoMapper in ein UserDto umgewandelt. Wenn nicht gefunden → gibt null zurück.
        public async Task<UserDto?> GetUserDetailAsync(int id)
        {
            var user = await _db.Users.Include(u => u.Role)
                                      .FirstOrDefaultAsync(u => u.Id == id);
            return user is null ? null : _mapper.Map<UserDto>(user);
        }
    }


    //Holt einen Benutzer mit seiner Rolle,
    //Gibt ihn ans Frontend zurück(oder null, wenn es ihn nicht gibt)
}

/*  
 Admin Service --Benutzer Service 
Was machst die Service??
Funktion : GetUserListAsysc
  - Hollt alle Benutzer aus der Datenbank
  - Lädt auch ihre Rolle mit (QM, Restaurant-Manger)
  - Optionaler Filter: Name , Email, Vorname -> suchfeld im Frontend
  - Konviertiert Daten zu UserDto für Frontend 
  ---> Gibts eine Liste von Benutzer zurück --gefiltert oder komplett
 
Funktion: GetUserDetailAsync(id)

  - Hollt einen Benutzer per ID
  - Mit zugehörigenRolle
  - Gibt ihn als UserDto zurück
  ---> Liefert die Benutzerdaten für die Detailseite



 
 
 */