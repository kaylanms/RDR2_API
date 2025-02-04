using RDR2.Domain.Entities;
using RDR2.Domain.Models;

namespace RDR2.Domain.Repositories;

public interface IMissionRepository
{
    Task CreateAsync(Mission mission);
    Task<List<MissionResponse>> GetAllAsync();
    Task<MissionResponse> GetByIdAsync(Guid id);
    Task<Mission> GetMissionByIdAsync(Guid id);
    Task<MissionResponse> UpdateAsync(Mission mission);
    Task<Guid> RemoveAsync(Guid id);
}