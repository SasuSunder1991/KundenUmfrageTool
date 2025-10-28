namespace KundenUmfrageTool.Api.Models
{
    public class Checkpoint
    {
        public long Id { get; set; }  // Datentyp long verwendet, um auch bei vielen Datensätzen(z.B Millionen Bewertungen) eine sichere Identifikation zu gewährleisten
        public string Name { get; set; } = string.Empty;

        // max. 300 Zeichen laut Lastenheft
        public string? Description { get; set; }
    }


}
