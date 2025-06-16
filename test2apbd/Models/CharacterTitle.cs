using System.ComponentModel.DataAnnotations.Schema;

namespace test2apbd.Models;

public class CharacterTitle
{
    public int CharacterId { get; set; }
    public int TitleId { get; set; }

    public DateTime AcquiredAt { get; set; }

    [ForeignKey(nameof(CharacterId))]
    public Character Character { get; set; } = null!;

    [ForeignKey(nameof(TitleId))]
    public Title Title { get; set; } = null!;
}