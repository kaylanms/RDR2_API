using RDR2.Application.Abstractions;
using RDR2.Domain.Repositories;

namespace RDR2.Application.Characters.Commands.MarkCharacterAsDead;

public sealed class MarkCharacterAsDeadCommandHandler : ICommandHandler<MarkCharacterAsDeadCommand, Guid>
{
    private readonly ICharacterRepository _characterRepository;
    private readonly IGangRepository _gangRepository;

    public MarkCharacterAsDeadCommandHandler(ICharacterRepository characterRepository, IGangRepository gangRepository)
    {
        _characterRepository = characterRepository;
        _gangRepository = gangRepository;
    }

    public async Task<Guid> Handle(MarkCharacterAsDeadCommand request, CancellationToken cancellationToken)
    {
        var character = await _characterRepository.GetCharacterByIdAsync(request.Id);
        
        if (character.GangId.HasValue)
        {
            var gang = await _gangRepository.GetGangByIdAsync(character.GangId.Value);
            gang.RemoveCharacter(character);
        }

        character.MarkAsDead();
        await _characterRepository.SaveAsync();
        
        return character.Id;
    }
}
