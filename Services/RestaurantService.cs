using AutoMapper;
using KundenUmfrageTool.Api.Data;
using KundenUmfrageTool.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace KundenUmfrageTool.Api.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly AppDbContext _db;      // Datenbankkontext (EF Core)
        private readonly IMapper _mapper;       // AutoMapper für Model → DTO

        public RestaurantService(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        // GetAllAsync() – QM sieht ALLE Restaurants
        public async Task<List<RestaurantDto>> GetAllAsync()
        {
            var list = await _db.Restaurants
                // Manager-Daten mitladen (JOIN)
                .Include(r => r.ManagerUser)
                // Bewertungen mitladen
                .Include(r => r.Ratings)
                // Zugeordnete Umfragen mitladen
                .Include(r => r.Surveys)
                .ToListAsync();

            return _mapper.Map<List<RestaurantDto>>(list);
        }
        //GetForManagerAsync() – Restaurant-Manager sieht nur SEIN Restaurant
        // --------------------------------------------------------------------
        // Filter: r.ManagerUserId == managerUserId
        // Dadurch sieht ein Restaurant-Manager nur sein zugewiesenes Restaurant.
        // --------------------------------------------------------------------
        public async Task<List<RestaurantDto>> GetForManagerAsync(int managerUserId)
        {
            var list = await _db.Restaurants

                .Include(r => r.ManagerUser)
                .Include(r => r.Ratings)
                .Include(r => r.Surveys)
                .Where(r => r.ManagerUserId == managerUserId)
                .ToListAsync();

            return _mapper.Map<List<RestaurantDto>>(list);
        }
        // Findet ein Restaurant per ID – wird z.B. für Detailseiten oder Bearbeiten gebraucht.
        public async Task<RestaurantDto?> GetByIdAsync(int id)
        {
            var entity = await _db.Restaurants
                .Include(r => r.ManagerUser)
                .Include(r => r.Ratings)
                .Include(r => r.Surveys)
                .FirstOrDefaultAsync(r => r.Id == id);

            return entity == null ? null :
                _mapper.Map<RestaurantDto>(entity);
        }
        // --------------------------------------------------------------------
        // DTO → Entity   Entity wird gespeichert
        // Entity → DTO zurück ans Frontend
        // --------------------------------------------------------------------
        public async Task<RestaurantDto> CreateAsync(RestaurantDto dto)
        {
            var entity = _mapper.Map<Restaurant>(dto);

            _db.Restaurants.Add(entity);
            await _db.SaveChangesAsync();

            return _mapper.Map<RestaurantDto>(entity);
        }
        // --------------------------------------------------------------------
        // Holt existierende DB-Entity          AutoMapper überträgt Änderungen darauf
        // Speichert im DB-Kontext
        // --------------------------------------------------------------------
        public async Task UpdateAsync(int id, RestaurantDto dto)
        {
            var entity = await _db.Restaurants.FindAsync(id);
            if (entity == null)
                throw new KeyNotFoundException("Restaurant nicht gefunden.");

            _mapper.Map(dto, entity);
            await _db.SaveChangesAsync();
        }


        // --------------------------------------------------------------------
        // Holt Entity        Löscht in EF Core
        // Speichert Änderung
        // --------------------------------------------------------------------



        public async Task DeleteAsync(int id)
        {
            var entity = await _db.Restaurants.FindAsync(id);
            if (entity == null)
                throw new KeyNotFoundException("Restaurant nicht gefunden.");

            _db.Restaurants.Remove(entity);
            await _db.SaveChangesAsync();
        }
    }
}




/* 
RestaurantService – Restaurant-Verwaltung

Dieser Service enthält die komplette Geschäftslogik für Restaurants.
 
* GetAllAsync() ---> QM sieht ALLE Restaurants.

- Wird vom QM-Manager benutzt

- Lädt:

   - Restaurant

   - ManagerUser

  - Ratings (Bewertungen)

  - Surveys (Umfragen)

  - Gibt Liste als DTO zurück
   


* GetForManagerAsync(ManagerUserId) ---> Restaurant-Manager sieht nur seine eigene Restaurant
 - wird vom Restaurant-Manger benutzt
 
  - Filter: nur Restaurant Manager benutzt
   
  - Filter: nur wo ManagerUserId === aktuelle ID



 * GetByIdAsync(id) --> Wird bei Detailseite oder zum Bearbeiten Nutzen

- Holt Restaurant mit Alle Info()Manager, Bewertungen , Umfrage
Wird bei Detailseite oder zum Bearbeiten Nutzen



* CreateAsynx(dto)  --> Restaurant anlegen
  
 - Wandelt DTO   ---> Entity
  - Speichert Restaurant in DB
  - Gibt DTO zurück



* UpdateAsync(id, dto)   --> Restaurant bearbeiten.
- Holt Bestehendes Restaurant
-Auto Manpper überscreibet die Felder
- Speichert Änderungen
 



* DeleteAsync(id)  --> Restaurant löschen.

  -  Holt Entity
  - Löscht aus DB

 */