using System.ComponentModel.DataAnnotations.Schema;

namespace test2apbd.Models;

public class Backpack
{
    public int CharacterId { get; set; }
    public int ItemId { get; set; }

    public int Amount { get; set; }

    // Navigation
    [ForeignKey(nameof(CharacterId))]
    public Character Character { get; set; } = null!;

    [ForeignKey(nameof(ItemId))]
    public Item Item { get; set; } = null!;
}