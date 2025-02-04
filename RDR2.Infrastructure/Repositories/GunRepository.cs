using Microsoft.EntityFrameworkCore;
using RDR2.Domain.Entities;
using RDR2.Domain.Exceptions.Gun;
using RDR2.Domain.Models;
using RDR2.Domain.Repositories;

namespace RDR2.Infrastructure.Repositories;

public class GunRepository : IGunRepository
{
    private readonly ApplicationDbContext _dbContext;

    public GunRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;



    public async Task<List<GunResponse>> GetAllAsync()
    {
        var response = await _dbContext.Guns.AsNoTracking().Select(g => new GunResponse(
            g.Id,
            g.Name,
            g.Damage,
            g.Ammunition,
            g.Cost)).ToListAsync();

        return response;
    }

    public async Task<GunResponse> GetByIdAsync(Guid id)
    {
        var gun = await FindGunOrThrowGunNotFoundException(id);
        return new GunResponse(gun.Id, gun.Name, gun.Damage, gun.Ammunition, gun.Cost); ;
    }

    public async Task<Gun> GetGunByIdAsync(Guid id)
        => await FindGunOrThrowGunNotFoundException(id);


    public async Task CreateAsync(Gun gun)
    {
        await _dbContext.Guns.AddAsync(gun);
        await _dbContext.SaveChangesAsync();
    }
    private async Task<Gun> FindGunOrThrowGunNotFoundException(Guid id) 
        => await _dbContext.Guns.FindAsync(id) ?? throw new GunNotFoundException(id);
}