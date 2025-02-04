namespace RDR2.Application.Characters.Commands.CreateCharacter;

public record CreateCharacterRequest(
    string FirstName,
    string LastName,
    int Age,
    bool IsAlive);