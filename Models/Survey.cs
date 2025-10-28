// Repräsentiert eine Umfrage (z. B. "Standard Umfrage")
// Eine Umfrage besteht aus mehreren Bewertungsbereichen (Checkpoints)
using KundenUmfrageTool.Api.Models;

public class Survey
{
    public long Id { get; set; } // Eindeutige ID der Umfrage
    public string Name { get; set; } = string.Empty; // Name der Umfrage
    public string? Description { get; set; } // Optionale Beschreibung
    // Liste der zugehörigen Checkpoints (Verknüpfung über SurveyCheckpoint)
    public ICollection<SurveyCheckpoint> SurveyCheckpoints { get; set; } = new List<SurveyCheckpoint>();
}

// Verbindungstabelle zwischen Survey und Checkpoint (n:m-Beziehung)
public class SurveyCheckpoint
{
    
    public long Id { get; set; }   // Primärschlüssel
    public Survey Survey { get; set; } = default!; // Zugehörige Umfrage
    public long CheckpointId { get; set; } // ID des Checkpoints
    public Checkpoint Checkpoint { get; set; } = default!; // Zugehöriger Bereich
}
