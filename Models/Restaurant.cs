namespace KundenUmfrageTool.Api.Models
{
    public class Restaurant
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? City { get; set; }
        public string? Country { get; set; }
        public long? ManagerUserId { get; set; }

        // Der Benutzer (AppUser), der als Manager für dieses Restaurant verantwortlich ist.
        // Ein Restaurant hat genau einen Manager, ein Manager gehört genau zu einem Restaurant.
        // Kann null sein, wenn noch kein Manager zugeordnet wurde.
        public AppUser? ManagerUser { get; set; }
        public long? SurveyId { get; set; }

        // Die Umfrage (Survey), die diesem Restaurant zugeordnet ist.
        // Kann null sein, wenn für dieses Restaurant noch keine Umfrage hinterlegt wurde.
        public Survey? Survey { get; set; }
    }
}
