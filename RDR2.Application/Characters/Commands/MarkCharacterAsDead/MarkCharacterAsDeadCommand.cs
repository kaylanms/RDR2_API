using RDR2.Application.Abstractions;

namespace RDR2.Application.Characters.Commands.MarkCharacterAsDead;

public record MarkCharacterAsDeadCommand(Guid Id) : ICommand<Guid>;