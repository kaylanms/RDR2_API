using System.Runtime.CompilerServices;
using RDR2.Application.Abstractions;
using RDR2.Domain.Models;
using RDR2.Domain.Repositories;

namespace RDR2.Application.Missions.Queries.GetMissionById;
internal sealed class GetMissionByIdQueryHandler : IQueryHandler<GetMissionByIdQuery, MissionResponse>
{
    private readonly IMissionRepository _repository;

    public GetMissionByIdQueryHandler(IMissionRepository repository) => _repository = repository;

    public async Task<MissionResponse> Handle(GetMissionByIdQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(request.Id);
    }
}
