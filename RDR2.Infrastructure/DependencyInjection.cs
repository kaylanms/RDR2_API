using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RDR2.Domain.Repositories;
using RDR2.Infrastructure.Repositories;

namespace RDR2.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ICharacterRepository, CharacterRepository>();
        services.AddScoped<IGunRepository, GunRepository>();
        services.AddScoped<IMissionRepository, MissionRepository>();
        services.AddScoped<IGangRepository, GangRepository>();

        // services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("Database"));
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        return services;
    }
}