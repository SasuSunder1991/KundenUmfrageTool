namespace KundenUmfrageTool.Api.Models
{
    public class Rating
    {
        public long Id { get; set; }                                              // long → weil du viele Datensätze erwartest (mehr als 2 Milliarden ist möglich).
        public long RestaurantId { get; set; }
        public long SurveyId { get; set; }
        public long CheckpointId { get; set; }
        public byte Score { get; set; }                                         // byte → für Werte zwischen 1 und 5 perfekt (klein, schnell, effizient).
        public string? Comment { get; set; }                                    // string? → ? bedeutet: kann leer (null) sein → Kommentar ist optional.
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;               //DateTime.UtcNow → speichert die Zeit unabhängig von Zeitzonen.
    }
}