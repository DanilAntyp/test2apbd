using test2apbd.DTOs;

namespace test2apbd.Serivces;

public interface IDbService
{
    Task<IEnumerable<CharacterDto>> GetAllCharactersAsync();
    Task<CharacterDto?> GetCharacterByIdAsync(int characterId);
    Task<OperationResult> AddItemsToBackpackAsync(int characterId, IEnumerable<int> itemIds);

}
public class OperationResult
{
    public bool Success { get; set; }
    public string? ErrorMessage { get; set; }

    public static OperationResult Fail(string message) => new OperationResult { Success = false, ErrorMessage = message };
    public static OperationResult Ok() => new OperationResult { Success = true };
}