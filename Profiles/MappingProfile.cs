using AutoMapper;
using KundenUmfrageTool.Api.Dtos;
using KundenUmfrageTool.Api.Dtos.Users;
using KundenUmfrageTool.Api.Models;

namespace KundenUmfrageTool.Api.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // User -> List DTO
            CreateMap<User, UserListDto>()
               .ForMember(d => d.FullName, o => o.MapFrom(s => $"{s.FirstName} {s.LastName}"))
               .ForMember(d => d.Role, o => o.MapFrom(s => s.Role != null ? s.Role.Name : ""));
            // ternärer Opertor prüfe hat der benutzer wirklich eine Role? Wenn ja -> nimm den Text s.Role.Name   wenn nein -> gib eine leerren string 



            // User -> Detail DTO
            CreateMap<User, UserDetailDto>();



            // Created DTO -> User
            CreateMap<CreateUserDto, User>()
                .ForMember(u => u.Id, o => o.Ignore())
                .ForMember(u => u.PasswordHash, o => o.Ignore())
                .ForMember(u => u.IsActive, o => o.MapFrom(_ => true));

            /*Wenn eine neuer Benutzer angelegen wird (CreateuserDto -> User )dann setze Is Active Immer automatisch auf true*
             * Warum machen wir das? weil
             * ein neu angelegt Benutzer immer aktiv sein soll
             * das Frontend dafür keine extra Feld Braucht
             *  es nicht manuell setzen musst
             * die Datenbank ein sauberers Standverhalten hat
             * AutoMapper setzt es automatisch: user.IsActive = true;

             * Prefekt für Soft Delete
             * 
             */













            //UPDATE DTO --> User
            CreateMap<UpdateUserDto, User>()
                .ForMember(u => u.Email, o => o.Ignore())    // Email darf Nicht geändert werden 
                .ForMember(u => u.PasswordHash, o => o.Ignore());   // Password ändern extar Feature   PasswordHash NIEMALS automatisch aus dem DTO überschreiben!

            // RestaurantDto → Restaurant (für CREATE / UPDATE)
            // Nur einfache Felder, keine Navigationen überschreiben!
            //CreateMap<RestaurantDto, Restaurant>()
            //    .ForMember(e => e.Id,
            //        o => o.Ignore())               // Id kommt aus der URL / DB
            //    .ForMember(e => e.ManagerUser,
            //        o => o.Ignore())               // Navigation, nicht aus DTO setzen
            //    .ForMember(e => e.Ratings,
            //        o => o.Ignore())               // wird von Bewertungen befüllt
            //    .ForMember(e => e.Surveys,
            //        o => o.Ignore());              // Survey-Zuordnung machst du separat

            CreateMap<Restaurant, RestaurantDto>()
                .ForMember(d => d.ManagerName,
                    o => o.MapFrom(s => s.ManagerUser != null
                        ? $"{s.ManagerUser.FirstName} {s.ManagerUser.LastName}"
                        : null))
                .ForMember(d => d.SurveyName,
                    o => o.MapFrom(s =>
                        s.Surveys.FirstOrDefault() != null
                            ? s.Surveys.First().Name
                            : null))
                .ForMember(d => d.QrCodeKey,
                    o => o.MapFrom(s => s.QrCodeKey))
                .ForMember(d => d.RatingCount,
                    o => o.MapFrom(s => s.Ratings.Count))
                .ForMember(d => d.AverageRating,
                    o => o.MapFrom(s =>
                        s.Ratings.Any()
                            ? s.Ratings.Average(x => x.Score)
                            : (double?)null));

        }
    }
}


/*
 * Was ist Mapping?
  Mapping = Zuordung, Übertagung
  Mapping bedutet = Daten von einem Objekt in einem anderes Objekt Kopieren/ Umwandeln

  Was ist Automapper??
  AutoMapper ist eine Bibliothek, die genau dieses Mapping automatisch für dich übernimmt.
  Vorteile:
  AutoMapper macht automatisch den Rest.
  Du schreibst viel weniger Code.
  Alles ist sauber und einheitlich.

  Mapping bedeutet, dass wir Daten aus unseren Datenbankmodellen (Entities) in DTOs übertragen, die das Frontend braucht.
  AutoMapper nimmt uns die Arbeit ab und erstellt automatisch die DTOs, damit wir das nicht jedes Mal manuell programmieren müssen.
  Dadurch ist unser Code viel sauberer und sicherer.

  Das ist die Datei, die AutoMapper sagt:Wie verwandle ich Entities (DB-Modelle) in DTOs (API-Modelle) — und zurück?
  
 
 AutoMapper = automatisches Mapping-System für:

 User → UserDto

 Restaurant → RestaurantDto

 RestaurantDto → Restaurant

Das macht deinen Code extrem sauber und spart haufenweise Schreibarbeit.
Ohne dieses Mapping würden deine Antworten an das Frontend keine Role und keinen FullName enthalten.




Was bedeutet o.Ignore()?

➡ AutoMapper soll dieses Feld NICHT automatisch mappen.
➡ Es wird ignoriert.
➡ AutoMapper fasst diese Eigenschaft nicht an.

Beispiel:  .ForMember(e => e.Id, o => o.Ignore())
Beim Mapping vom DTO zu Restaurant → bitte die ID NICHT überschreiben.(When mapping from DTO to restaurant → please DO NOT overwrite the ID.)

Warum braucht man Ignore()?

Weil manche Felder NICHT vom DTO stammen sollen.

Beispiele:
1. Id

Die ID kommt aus der Datenbank, nicht aus dem Frontend.

Ohne Ignore:
 Der Nutzer könnte die ID manipulieren
 Ein Update überschreibt fremde Einträge
 Sicherheitsproblem
 

Ohne Ignore:

AutoMapper, bitte alles von DTO auf Entity kopieren.

Mit Ignore:

AutoMapper, bitte kopiere alles außer diesem Feld.
 */


































/* 
 Was ist Mapping
Mapping = Daten von einem Objekt in anderes übertragen

User aus der Datenbank hat viele Felder(PasswordHash, RoleID, CreatedAt) UserDto für Fronetend Braucht nur Name , Email, Rolle

Warum 2 Mappings 
Enity -> DTO
Hier Holst Daten aus der Datenbank
und baust daraus das DTO fürs Frontend
AutoMapper  übertragt die Felder automatisch



DTO--> Entity 
Wenn man ein neues Restaurant anlegen(POST)
oder ein Restaurant Bearbeitetst (PUT)

 Dann kommt das DTO vom Frontend
und du musst es ins Entity übertragen:

Name

Street

ZipCode

City

Country

ManagerUserId

Aber NICHT:

Navigationen (ManagerUser)

Navigationen (Surveys)

Ratings

Id (niemals überschreiben!)

Dieses Mapping wird bei CREATE / UPDATE verwendet
 
 
 */