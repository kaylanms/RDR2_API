using RDR2.Application.Abstractions;
using RDR2.Domain.Models;
using RDR2.Domain.Repositories;

namespace RDR2.Application.Gangs.Queries.GetAllGangs;

internal sealed class GetAllGangsQueryHandler : IQueryHandler<GetAllGangsQuery, List<GangResponse>>
{
   private readonly IGangRepository _repository;

    public GetAllGangsQueryHandler(IGangRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<GangResponse>> Handle(GetAllGangsQuery request, CancellationToken cancellationToken) 
        => await _repository.GetAllAsync();
}
