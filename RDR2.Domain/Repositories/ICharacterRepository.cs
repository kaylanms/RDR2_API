using RDR2.Domain.Entities;
using RDR2.Domain.Models;

namespace RDR2.Domain.Repositories;

public interface ICharacterRepository
{
    Task CreateAsync(Character character);
    Task<List<CharacterResponse>> GetAllAsync();
    Task<CharacterResponse> GetByIdAsync(Guid id);
    Task<Character> GetCharacterByIdAsync(Guid id);
    Task SaveAsync();
}