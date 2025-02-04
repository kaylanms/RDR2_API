using Microsoft.EntityFrameworkCore;
using RDR2.Domain.Entities;
using RDR2.Domain.Exceptions.Mission;
using RDR2.Domain.Models;
using RDR2.Domain.Repositories;

namespace RDR2.Infrastructure.Repositories;

public class MissionRepository : IMissionRepository
{
    private readonly ApplicationDbContext _dbContext;

    public MissionRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

    public async Task CreateAsync(Mission mission)
    {
        await _dbContext.Missions.AddAsync(mission);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<MissionResponse>> GetAllAsync()
        => await _dbContext.Missions.AsNoTracking().Select(m => new MissionResponse(
            m.Id,
            m.Name,
            m.Overview,
            m.IsPrimary)).ToListAsync();

    public async Task<MissionResponse> GetByIdAsync(Guid id)
    {
        var mission = await FindMissionOrThrowMissionNotFoundException(id);
        return new MissionResponse(mission.Id, mission.Name, mission.Overview, mission.IsPrimary);
    }

    public async Task<Mission> GetMissionByIdAsync(Guid id) => await FindMissionOrThrowMissionNotFoundException(id);


    public async Task<MissionResponse> UpdateAsync(Mission mission)
    {
        _dbContext.Entry(mission).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
        return new MissionResponse(mission.Id, mission.Name, mission.Overview, mission.IsPrimary);
    }
    public async Task<Guid> RemoveAsync(Guid id)
    {
        _dbContext.Missions.Remove(await GetMissionByIdAsync(id));
        await _dbContext.SaveChangesAsync();
        return id;
    }
    private async Task<Mission> FindMissionOrThrowMissionNotFoundException(Guid id) 
        => await _dbContext.Missions.FindAsync(id) ?? throw new MissionNotFoundException(id);
}