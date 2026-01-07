namespace KundenUmfrageTool.Api.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Checkpoints")]
public class Checkpoint
{
    [Key]
    public int Id { get; set; }

    [Required]                    //Name darf nicht leer sein
    [MaxLength(200)]           
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




 
public ICollection<SurveyCheckpoint> SurveyCheckpoints { get; set; } = new List<SurveyCheckpoint>();

Das ist eine Sammlung/ Liste von SurveyCheckpoint- Objekten
ICollection<T> ist der Typ
List<T> ist die korrekte Liste , die direkt erstellt wird 

Warum nicht einfach List<surveyCheckpoint> Schrreiben??
Weil ICollection flexiber ist
- ICollection<T> = schnittstelle
- List <T>(= konkrete Implementierung)
Beispiel:
Du gibst außen nur die „Fähigkeit“ an (ICollection = Sammlung),
aber intern entscheidest du, wie es gespeichert wird (List = konkrete Liste).


 
 ICollection = eine Sammlung, in der mehrere Elemente drin sind
 new List = ich erzeuge eine echte Liste
 
 
 Warum ist hier eine Liste nötig?

Weil diese Eigenschaft eine 1:n oder n:m Beziehung beschreibt.

Beispiel:
Eine Umfrage hat mehrere Checkpoints → deshalb brauchst du eine Sammlung.
Ja, das ist eine Liste.
ICollection<SurveyCheckpoint> bedeutet: die Umfrage besitzt mehrere SurveyCheckpoint-Einträge.
Wir initialisieren es direkt mit new List<SurveyCheckpoint>(), damit die Liste nie null ist und EF Core sauber damit arbeiten kann.
 */