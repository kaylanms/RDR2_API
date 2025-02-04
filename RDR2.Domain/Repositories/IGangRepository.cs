using RDR2.Domain.Entities;
using RDR2.Domain.Models;

namespace RDR2.Domain.Repositories;

public interface IGangRepository
{
    Task CreateAsync(Gang gang);
    Task<List<GangResponse>> GetAllAsync();
    Task<Gang> GetGangByIdAsync(Guid id);
    Task SaveAsync();
}