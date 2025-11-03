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

        public ICollection<SurveyCheckpoint> SurveyCheckpoints { get; set; } = new List<SurveyCheckpoint>();
    }
}
