using RDR2.Application.Abstractions;
using RDR2.Domain.Entities;
using RDR2.Domain.Repositories;

namespace RDR2.Application.Gangs.Commands.CreateGang;

internal sealed class CreateGangCommandHandler : ICommandHandler<CreateGangCommand, Guid>
{
    private readonly IGangRepository _gangRepository;
    private readonly ICharacterRepository _characterRepository;

    public CreateGangCommandHandler(IGangRepository gangRepository, ICharacterRepository characterRepository)
    {
        _gangRepository = gangRepository;
        _characterRepository = characterRepository;
    }

    public async Task<Guid> Handle(CreateGangCommand request, CancellationToken cancellationToken)
    {
        var leader = await _characterRepository.GetCharacterByIdAsync(request.LeaderId);
        var gang = new Gang(Guid.NewGuid(), request.Name, request.LeaderId);
        gang.AddCharacter(leader);

        await _gangRepository.CreateAsync(gang);
        
        return gang.Id;
    }
}
