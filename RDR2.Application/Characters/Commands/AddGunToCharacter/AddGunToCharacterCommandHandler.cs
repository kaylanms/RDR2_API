using RDR2.Application.Abstractions;
using RDR2.Application.Characters.Commands.AddGunToCharacter;
using RDR2.Domain.Entities;
using RDR2.Domain.Repositories;

public sealed class AddGunToCharacterCommandHandler : ICommandHandler<AddGunToCharacterCommand, Guid>
{
    private readonly ICharacterRepository _characterRepository;

    public AddGunToCharacterCommandHandler(ICharacterRepository characterRepository)
    {
        _characterRepository = characterRepository;
    }

    public async Task<Guid> Handle(AddGunToCharacterCommand request, CancellationToken cancellationToken)
    {
        var character = await _characterRepository.GetCharacterByIdAsync(request.CharacterId);

        if (!character.IsAlive)
        {
            throw new InvalidOperationException("Cannot add a gun to a dead character.");
        }

        if (character.CharacterGuns.Any(cg => cg.GunId == request.GunId))
        {
            return request.GunId;
        }

        var characterGun = new CharacterGun
        {
            CharacterId = request.CharacterId,
            GunId = request.GunId
        };

        character.CharacterGuns.Add(characterGun);

        await _characterRepository.SaveAsync();

        return characterGun.GunId;
    }
}
