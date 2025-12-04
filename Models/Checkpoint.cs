namespace KundenUmfrageTool.Api.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Checkpoints")]
public class Checkpoint
{
    [Key]
    public int Id { get; set; }

    [Required]                    //Name darf nicht leer sein
    [MaxLength(200)]              // sinvolle obergrenze für Anzeige/listen(optional)
    public string Name { get; set; } = string.Empty;

    [MaxLength(300)]
    public string? Description { get; set; }


    //  Beziehung zu SurveyCheckpoints (n:m)
    public ICollection<SurveyCheckpoint> SurveyCheckpoints { get; set; } = new List<SurveyCheckpoint>();

}


/*
 Ein Checkpoint ist ein Bewertungsbereich, z. B.:

* Service

* Getränke

* Essen

* Ambiente

Ein Checkpoint kann:

* in mehreren Umfragen vorkommen (z. B. Service gibt es in jeder Umfrage)

* eine optionale Beschreibung haben

„SurveyCheckpoints“ ist ein n:m Join, der eine Checkpoint-Liste an eine Survey bindet.
 
 
 
 
 
 
 
 */