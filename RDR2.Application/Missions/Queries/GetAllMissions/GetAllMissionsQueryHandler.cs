using RDR2.Application.Abstractions;
using RDR2.Domain.Models;
using RDR2.Domain.Repositories;

namespace RDR2.Application.Missions.Queries.GetAllMissions;
internal sealed class GetAllMissionsQueryHandler : IQueryHandler<GetAllMissionsQuery, List<MissionResponse>>
{
    private readonly IMissionRepository _repository;

    public GetAllMissionsQueryHandler(IMissionRepository repository) => _repository = repository;

    public async Task<List<MissionResponse>> Handle(GetAllMissionsQuery request, CancellationToken cancellationToken) 
        => await _repository.GetAllAsync();
}