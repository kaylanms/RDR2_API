using Microsoft.EntityFrameworkCore;
using RDR2.Domain.Entities;
using RDR2.Domain.Exceptions.Gang;
using RDR2.Domain.Models;
using RDR2.Domain.Repositories;

namespace RDR2.Infrastructure.Repositories;

public class GangRepository : IGangRepository
{
    private readonly ApplicationDbContext _dbContext;

    public GangRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;
    public async Task<List<GangResponse>> GetAllAsync()
    {
        return await _dbContext.Gangs.AsNoTracking()
            .Select(g => new GangResponse(g.Id, g.Name, g.Leader.FirstName, g.TotalMembers))
            .ToListAsync();
    }
    public async Task<Gang> GetGangByIdAsync(Guid id)
        => await _dbContext.Gangs.FindAsync(id) ?? throw new GangNotFoundException(id);

    public async Task CreateAsync(Gang gang)
    {
        await _dbContext.Gangs.AddAsync(gang);
        await _dbContext.SaveChangesAsync();
    }

    public async Task SaveAsync() => await _dbContext.SaveChangesAsync();
}
