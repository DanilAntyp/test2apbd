using System.ComponentModel.DataAnnotations;

namespace test2apbd.Models;

public class Character
{
    [Key]
    public int CharacterId { get; set; }

    [MaxLength(50)]
    public string FirstName { get; set; } = string.Empty;

    [MaxLength(120)]
    public string LastName { get; set; } = string.Empty;

    public int CurrentWeight { get; set; }
    public int MaxWeight { get; set; }

    // Navigation
    public ICollection<Backpack> Backpacks { get; set; } = new HashSet<Backpack>();
    public ICollection<CharacterTitle> CharacterTitles { get; set; } = new HashSet<CharacterTitle>();
}