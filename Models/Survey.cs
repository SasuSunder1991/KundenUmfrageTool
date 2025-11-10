using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KundenUmfrageTool.Api.Models
{
    [Table("Surveys")]
    public class Survey
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(150)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; }

        // Jede Umfrage gehört zu genau einem Restaurant
        [ForeignKey(nameof(Restaurant))]
        public int RestaurantId { get; set; }  // nicht nullable
        public Restaurant? Restaurant { get; set; }

        // Eine Umfrage hat mehrere Checkpoints
        public ICollection<SurveyCheckpoint> SurveyCheckpoints { get; set; } = new List<SurveyCheckpoint>();
    }
}
