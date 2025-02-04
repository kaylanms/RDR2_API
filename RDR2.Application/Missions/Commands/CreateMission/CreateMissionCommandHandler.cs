using RDR2.Application.Abstractions;
using RDR2.Domain.Entities;
using RDR2.Domain.Repositories;

namespace RDR2.Application.Missions.Commands.CreateMission;
internal sealed class CreateMissionCommandHandler : ICommandHandler<CreateMissionCommand, Guid>
{
    private readonly IMissionRepository _repository;

    public CreateMissionCommandHandler(IMissionRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateMissionCommand request, CancellationToken cancellationToken)
    {
        var mission = new Mission(
            Guid.NewGuid(),
            request.Name,
            request.Overview,
            request.IsPrimary);

        await _repository.CreateAsync(mission);

        return mission.Id;
    }

}
