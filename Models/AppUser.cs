namespace KundenUmfrageTool.Api.Models
{
    public class AppUser
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public int RoleId { get; set; }                                 // Die Zahl aus der Tabelle Role (z. B. 1 oder 2)
        public Role? Role { get; set; }                                 // Das ganze Role-Objekt (Name, Beschreibung usw.)  ? Fragezeichen bedeutet : Diese Eigenschaft kann eventuell leer(null) sein
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}


