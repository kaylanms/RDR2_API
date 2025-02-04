using Microsoft.EntityFrameworkCore;
using RDR2.Domain.Entities;
using RDR2.Domain.Exceptions.Character;
using RDR2.Domain.Models;
using RDR2.Domain.Repositories;

namespace RDR2.Infrastructure.Repositories;

public class CharacterRepository : ICharacterRepository
{
    private readonly ApplicationDbContext _dbContext;

    public CharacterRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;


    public async Task<List<CharacterResponse>> GetAllAsync()
    {
        var response = await _dbContext.Characters.AsNoTracking()
            .Include(c => c.Gang)
            .Include(c => c.CharacterGuns)  // Incluir a tabela intermediária CharacterGun
            .ThenInclude(cg => cg.Gun)     // Incluir a tabela Gun associada à CharacterGun
            .Select(c => new CharacterResponse
            (
                c.Id,
                c.FirstName,
                c.LastName,
                c.Age,
                c.IsAlive,
                c.Gang != null ? new GangResponse(c.Gang.Id, c.Gang.Name, c.FirstName, c.Gang.TotalMembers) : null,
                c.CharacterGuns.Select(cg => new GunResponse
                (
                    cg.Gun.Id,
                    cg.Gun.Name,
                    cg.Gun.Damage,
                    cg.Gun.Ammunition,
                    cg.Gun.Cost
                )).ToList()
            ))
            .ToListAsync();

        return response;
    }

    public async Task<CharacterResponse> GetByIdAsync(Guid id)
    {
        var character = await _dbContext.Characters
            .Include(c => c.CharacterGuns)  // Inclui as armas associadas
            .ThenInclude(cg => cg.Gun)     // Inclui as armas de fato associadas
            .FirstOrDefaultAsync(c => c.Id == id) // Busca pelo personagem
            ?? throw new CharacterNotFoundException(id);

        // Criar o CharacterResponse, convertendo a lista de Guns para GunResponse
        var response = new CharacterResponse(
            character.Id,
            character.FirstName,
            character.LastName,
            character.Age,
            character.IsAlive,
            character.Gang != null ? new GangResponse(
                character.Gang.Id,
                character.Gang.Name,
                character.FirstName,
                character.Gang.TotalMembers) : null,
            character.CharacterGuns.Select(cg => new GunResponse(
                cg.Gun.Id,
                cg.Gun.Name,
                cg.Gun.Damage,
                cg.Gun.Ammunition,
                cg.Gun.Cost
            )).ToList() // Converte a lista de CharacterGun para GunResponse
        );

        return response;
    }

    public async Task<Character> GetCharacterByIdAsync(Guid id)
        => await _dbContext.Characters.FindAsync(id) ?? throw new CharacterNotFoundException(id);
        
    public async Task CreateAsync(Character character)
    {
        await _dbContext.Characters.AddAsync(character);
        await _dbContext.SaveChangesAsync();
    }

    public async Task SaveAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}