using RDR2.Application.Abstractions;
using RDR2.Domain.Entities;
using RDR2.Domain.Models;
using RDR2.Domain.Repositories;

namespace RDR2.Application.Missions.Commands.UpdateMission;

internal sealed class UpdateMissionCommandHandler : ICommandHandler<UpdateMissionCommand, MissionResponse>
{
    private readonly IMissionRepository _repository;

    public UpdateMissionCommandHandler(IMissionRepository repository) => _repository = repository;

    public async Task<MissionResponse> Handle(UpdateMissionCommand request, CancellationToken cancellationToken) 
        => await _repository.UpdateAsync(new Mission(
            request.Id,
            request.Name,
            request.Overview,
            request.IsPrimary));
}
