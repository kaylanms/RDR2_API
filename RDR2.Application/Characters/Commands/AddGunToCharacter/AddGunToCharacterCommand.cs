using RDR2.Application.Abstractions;

namespace RDR2.Application.Characters.Commands.AddGunToCharacter;

public record AddGunToCharacterCommand(Guid CharacterId, Guid GunId) : ICommand<Guid>;