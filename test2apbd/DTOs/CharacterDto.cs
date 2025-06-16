namespace test2apbd.DTOs;

public class CharacterDto
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public int CurrentWeight { get; set; }
    public int MaxWeight { get; set; }

    public IEnumerable<BackpackItemDto> BackpackItems { get; set; } = new List<BackpackItemDto>();
    public IEnumerable<TitleDto> Titles { get; set; } = new List<TitleDto>();
}