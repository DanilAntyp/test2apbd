using Microsoft.EntityFrameworkCore;
using test2apbd.Data;
using test2apbd.DTOs;
using test2apbd.Models;

namespace test2apbd.Serivces;

public class DbService:IDbService
{
    private readonly DatabaseContext _context;

    public DbService(DatabaseContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<CharacterDto>> GetAllCharactersAsync()
    {
        return await _context.Characters
            .Include(c => c.Backpacks)
            .ThenInclude(b => b.Item)
            .Include(c => c.CharacterTitles)
            .ThenInclude(ct => ct.Title)
            .Select(c => new CharacterDto
            {
                FirstName     = c.FirstName,
                LastName      = c.LastName,
                CurrentWeight = c.CurrentWeight,
                MaxWeight     = c.MaxWeight,
                BackpackItems = c.Backpacks.Select(b => new BackpackItemDto
                {
                    ItemName   = b.Item.Name,
                    ItemWeight = b.Item.Weight,
                    Amount     = b.Amount
                }).ToList(),
                Titles = c.CharacterTitles.Select(ct => new TitleDto
                {
                    Title      = ct.Title.Name,
                    AcquiredAt = ct.AcquiredAt
                }).ToList()
            })
            .ToListAsync();
    }

    public async Task<CharacterDto?> GetCharacterByIdAsync(int characterId)
    {
        return await _context.Characters
            .Where(c => c.CharacterId == characterId)
            .Include(c => c.Backpacks)
            .ThenInclude(b => b.Item)
            .Include(c => c.CharacterTitles)
            .ThenInclude(ct => ct.Title)
            .Select(c => new CharacterDto
            {
                FirstName     = c.FirstName,
                LastName      = c.LastName,
                CurrentWeight = c.CurrentWeight,
                MaxWeight     = c.MaxWeight,
                BackpackItems = c.Backpacks.Select(b => new BackpackItemDto
                {
                    ItemName   = b.Item.Name,
                    ItemWeight = b.Item.Weight,
                    Amount     = b.Amount
                }).ToList(),
                Titles = c.CharacterTitles.Select(ct => new TitleDto
                {
                    Title      = ct.Title.Name,
                    AcquiredAt = ct.AcquiredAt
                }).ToList()
            })
            .SingleOrDefaultAsync();
    }

    public async Task<OperationResult> AddItemsToBackpackAsync(int characterId, IEnumerable<int> itemIds)
    {
        if (!itemIds.Any())
            return OperationResult.Fail("No item IDs passed.");

        var character = await _context.Characters
            .Include(c => c.Backpacks)
            .SingleOrDefaultAsync(c => c.CharacterId == characterId);

        if (character == null)
            return OperationResult.Fail("Character not found.");

        var items = await _context.Items
            .Where(i => itemIds.Contains(i.ItemId))
            .ToListAsync();

        if (items.Count != itemIds.Count())
            return OperationResult.Fail("items not found.");

        int totalWeightToAdd = items.Sum(i => i.Weight);

        if (character.CurrentWeight + totalWeightToAdd > character.MaxWeight)
            return OperationResult.Fail("no capasity to add items.");

        foreach (var item in items)
        {
            var backpackItem = character.Backpacks.FirstOrDefault(b => b.ItemId == item.ItemId);
            if (backpackItem != null)
            {
                backpackItem.Amount++;
            }
            else
            {
                _context.Backpacks.Add(new Backpack
                {
                    CharacterId = characterId,
                    ItemId = item.ItemId,
                    Amount = 1
                });
            }
        }

        character.CurrentWeight += totalWeightToAdd;
        await _context.SaveChangesAsync();

        return OperationResult.Ok();
    }
}

    