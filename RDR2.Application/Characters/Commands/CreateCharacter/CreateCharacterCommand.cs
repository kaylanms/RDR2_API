using RDR2.Application.Abstractions;

namespace RDR2.Application.Characters.Commands.CreateCharacter;

public record CreateCharacterCommand(
    string FirstName,
    string LastName,
    int Age,
    bool IsAlive) : ICommand<Guid>;