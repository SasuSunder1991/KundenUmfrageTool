using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KundenUmfrageTool.Api.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int Id { get; set; }

        // Vorname (Pflichtfeld)
        [Required, MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;

        // Nachname (Pflichtfeld)
        [Required, MaxLength(100)]
        public string LastName { get; set; } = string.Empty;

        // Email (Pflichtfeld, darf NICHT geändert werden)
        [Required]
        [EmailAddress(ErrorMessage = "Ungültige E-Mail-Adresse.")]
        [MaxLength(255)]
        public string Email { get; set; } = string.Empty;

        // Passwort-Hash (wird verschlüsselt gespeichert)
        [Required, MaxLength(255)]
        public string PasswordHash { get; set; } = string.Empty;

        // Rolle (Fremdschlüssel zur Tabelle Roles)
        [Required]
        public int RoleId { get; set; }       // z. B. 1 = QM-Manager, 2 = Restaurant-Manager

        [ForeignKey(nameof(RoleId))]
        public Role? Role { get; set; }       // Navigationseigenschaft → Zugriff auf die Rolleninfos

        // Aktivstatus
        public bool IsActive { get; set; } = true;

        // Erstellungsdatum (UTC)
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
