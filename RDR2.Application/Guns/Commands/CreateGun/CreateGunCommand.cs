using RDR2.Application.Abstractions;

namespace RDR2.Application.Guns.Commands.CreateGun;

public record CreateGunCommand(
    string Name,
    float Damage,
    int Ammunition,
    int Cost) : ICommand<Guid>;