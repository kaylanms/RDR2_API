namespace RDR2.Domain.Models;

public record GunResponse(
    Guid Id,
    string Name,
    float Damage,
    int Ammunition,
    int Cost);