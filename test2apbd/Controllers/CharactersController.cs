using Microsoft.AspNetCore.Mvc;
using test2apbd.DTOs;
using test2apbd.Serivces;

namespace test2apbd.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CharactersController : ControllerBase
{
    private readonly IDbService _dbService;

    public CharactersController(IDbService dbService)
    {
        _dbService = dbService;
    }

      
    [HttpGet("{characterId}")]
    public async Task<ActionResult<CharacterDto>> GetCharacter(int characterId)
    {
        var character = await _dbService.GetCharacterByIdAsync(characterId);
        if (character is null)
            return NotFound();

        return Ok(character);
    }
    
    
    [HttpPost("{characterId}/backpacks")]
    public async Task<IActionResult> AddItemsToBackpack(int characterId, [FromBody] List<int> itemIds)
    {
        if (itemIds == null || itemIds.Count == 0)
            return BadRequest("Item ID list cannot be empty.");

        var result = await _dbService.AddItemsToBackpackAsync(characterId, itemIds);

        if (!result.Success)
            return BadRequest(result.ErrorMessage);

        return Ok();
    }
}
