using RDR2.Application.Abstractions;

namespace RDR2.Application.Characters.Commands.AddCharacterToGang;

public sealed record AddCharacterToGangCommand(Guid CharacterId, Guid GangId) : ICommand<Guid>;