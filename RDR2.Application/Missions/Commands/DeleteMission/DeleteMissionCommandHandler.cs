using RDR2.Application.Abstractions;
using RDR2.Domain.Entities;
using RDR2.Domain.Repositories;

namespace RDR2.Application.Missions.Commands.DeleteMission;
internal sealed class DeleteMissionCommandHandler : ICommandHandler<DeleteMissionCommand, Guid>
{
    private readonly IMissionRepository _repository;

    public DeleteMissionCommandHandler(IMissionRepository repository) => _repository = repository;

    public async Task<Guid> Handle(DeleteMissionCommand request, CancellationToken cancellationToken)
        => await _repository.RemoveAsync(request.Id);
}
