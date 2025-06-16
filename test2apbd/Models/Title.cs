using System.ComponentModel.DataAnnotations;

namespace test2apbd.Models;

public class Title
{
    [Key]
    public int TitleId { get; set; }

    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    // Navigation
    public ICollection<CharacterTitle> CharacterTitles { get; set; } = new HashSet<CharacterTitle>();
}