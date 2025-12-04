using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KundenUmfrageTool.Api.Models
{
    [Table("SurveyCheckpoints")]
    public class SurveyCheckpoint
    {
        [Key]
        public int Id { get; set; }

        // Fremdschlüssel zur Survey
        [Required]
        public int SurveyId { get; set; }

        // Fremdschlüssel zum Checkpoint
        [Required]
        public int CheckpointId { get; set; }

        [ForeignKey(nameof(SurveyId))]
        public Survey? Survey { get; set; }

        [ForeignKey(nameof(CheckpointId))]
        public Checkpoint? Checkpoint { get; set; }
    }
}
/* 
 Eine Survey besteht aus mehreren Checkpoints.

Diese Tabelle macht folgende Verbindungen möglich:

Survey 1 -> Service
Survey 1 -> Essen
Survey 1 -> Getränke


Dadurch wird eine Umfrage flexibel definierbar.
 
 
 */