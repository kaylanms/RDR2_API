namespace RDR2.Application.Guns.Commands.CreateGun;

public record CreateGunRequest(
    string Name,
    float Damage,
    int Ammunition,
    int Cost);