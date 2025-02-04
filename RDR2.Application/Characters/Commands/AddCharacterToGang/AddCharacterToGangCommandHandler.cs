using RDR2.Application.Abstractions;
using RDR2.Domain.Repositories;

namespace RDR2.Application.Characters.Commands.AddCharacterToGang;

public sealed class AddCharacterToGangCommandHandler : ICommandHandler<AddCharacterToGangCommand, Guid>
{
    private readonly ICharacterRepository _characterRepository;
    private readonly IGangRepository _gangRepository;

    public AddCharacterToGangCommandHandler(ICharacterRepository characterRepository, IGangRepository gangRepository)
    {
        _characterRepository = characterRepository;
        _gangRepository = gangRepository;
    }

    public async Task<Guid> Handle(AddCharacterToGangCommand request, CancellationToken cancellationToken)
    {
        var character = await _characterRepository.GetCharacterByIdAsync(request.CharacterId);
        var gang = await _gangRepository.GetGangByIdAsync(request.GangId);

        character.JoinGang(gang);
        await _characterRepository.SaveAsync();

        return gang.Id;
    }
}
