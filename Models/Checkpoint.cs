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
}