namespace RDR2.Application.Characters.Commands.AddGunToCharacter;

public record AddGunToCharacterRequest(Guid CharacterId, Guid GunId);