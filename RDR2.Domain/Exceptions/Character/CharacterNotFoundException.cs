namespace RDR2.Domain.Exceptions.Character;

public class CharacterNotFoundException : Exception
{

    public CharacterNotFoundException(Guid characterId)
        : base($"Character with ID {characterId} not found.")
    {
    }
}