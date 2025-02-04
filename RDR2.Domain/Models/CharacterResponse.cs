using RDR2.Domain.Entities;

namespace RDR2.Domain.Models;

public sealed record CharacterResponse(
    Guid Id,
    string FirstName,
    string LastName,
    int Age,
    bool IsAlive,
    GangResponse? Gang,
    List<GunResponse> Guns);