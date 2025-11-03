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

        // Beziehung zu Umfrage (Survey)
        public int? SurveyId { get; set; }
        public Survey? Survey { get; set; }

        // Beziehung zu Bewertungen (Ratings)
        public ICollection<Rating> Ratings { get; set; } = new List<Rating>();
        public ICollection<Survey> Surveys { get; set; } = new List<Survey>();

    }
}
