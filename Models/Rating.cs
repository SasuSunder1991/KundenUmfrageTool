using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KundenUmfrageTool.Api.Models
{
    [Table("Ratings")]
    public class Rating
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int RestaurantId { get; set; }

        [Required]
        public int SurveyId { get; set; }

        [Required]
        public int CheckpointId { get; set; }

        [Range(1, 5)]
        public int Score { get; set; }

        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [ForeignKey(nameof(RestaurantId))]
        public Restaurant? Restaurant { get; set; }

        [ForeignKey(nameof(SurveyId))]
        public Survey? Survey { get; set; }

        [ForeignKey(nameof(CheckpointId))]
        public Checkpoint? Checkpoint { get; set; }
    }
}


/*
 
 Eine Bewertung besteht aus:

 * ⭐ Score (1–5)

* optionalem Kommentar

* Checkpoint (Service/Essen/...)

* Survey (Umfrage)

* Restaurant

Jede Kundenbewertung ist vollständig verbunden, daher kannst du später:

* Durchschnitt berechnen

* Beste/schlechteste Bewertung finden

*Kommentare nach Bereich gruppieren

 Diese Klasse bildet die Grundlage für die gesamte Auswertung!
 
 
 
 */