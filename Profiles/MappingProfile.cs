using AutoMapper;
using KundenUmfrageTool.Api.Dtos;
using KundenUmfrageTool.Api.Models;

namespace KundenUmfrageTool.Api.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // User -> UserDto
            CreateMap<User, UserDto>()
                .ForMember(d => d.FullName, o => o.MapFrom(s => $"{s.FirstName} {s.LastName}"))
                .ForMember(d => d.Role, o => o.MapFrom(s => s.Role != null ? s.Role.Name : ""));

            //Restaurant -> RestaurantDto (für GET / Anzeige im Frontend)
            CreateMap<Restaurant, RestaurantDto>()
                // Name & Adresse werden automatisch gemappt (Name, Street, ZipCode, City, Country)
                .ForMember(d => d.ManagerName,
                    o => o.MapFrom(s =>
                        s.ManagerUser != null
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




            // ✅ RestaurantDto → Restaurant (für CREATE / UPDATE)
            // Nur einfache Felder, keine Navigationen überschreiben!
            CreateMap<RestaurantDto, Restaurant>()
                .ForMember(e => e.Id,
                    o => o.Ignore())               // Id kommt aus der URL / DB
                .ForMember(e => e.ManagerUser,
                    o => o.Ignore())               // Navigation, nicht aus DTO setzen
                .ForMember(e => e.Ratings,
                    o => o.Ignore())               // wird von Bewertungen befüllt
                .ForMember(e => e.Surveys,
                    o => o.Ignore());              // Survey-Zuordnung machst du separat
        }
    }
}





































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