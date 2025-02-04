using RDR2.Domain.Entities;
using RDR2.Domain.Models;

namespace RDR2.Domain.Repositories;

public interface IGunRepository
{
    Task CreateAsync(Gun gun);
    Task<List<GunResponse>> GetAllAsync();
    Task<GunResponse> GetByIdAsync(Guid id);
    Task<Gun> GetGunByIdAsync(Guid id);
}