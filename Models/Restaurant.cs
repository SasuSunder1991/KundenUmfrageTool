using System.ComponentModel.DataAnnotations;

namespace KundenUmfrageTool.Api.Models
{
    public class Restaurant
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(200)]
        public string? Street { get; set; }

        [MaxLength(200)]
        public string? ZipCode { get; set; }

        [MaxLength(200)]
        public string? City { get; set; }

        [MaxLength(100)]
        public string? Country { get; set; }

        // Beziehung zu Manager (User)
        public int? ManagerUserId { get; set; }
        public User? ManagerUser { get; set; }

        // Beziehung zu Bewertungen (Ratings)
        public ICollection<Rating> Ratings { get; set; } = new List<Rating>();

        // Ein Restaurant kann mehrere Umfragen haben
        public ICollection<Survey> Surveys { get; set; } = new List<Survey>();

        [MaxLength(50)]
        public string? QrCodeKey { get; set; }

    }
}
/* 
 Ein Restaurant hat:

* Name

* Adresse

* ManagerUserId (Restaurant-Manager)

* Ratings (alle abgegebenen Bewertungen)

* Surveys (alle Umfragen, die diesem Restaurant gehören)

* QrCodeKey (wird später zum Generieren des QR-Codes verwendet)

Sehr wichtig für Auswertung:
Über Ratings kannst du alle Bewertungen des Restaurants abrufen.
 
 
 
 
 
 
 
 */