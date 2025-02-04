using RDR2.Application.Abstractions;
using RDR2.Domain.Models;
using RDR2.Domain.Repositories;

namespace RDR2.Application.Guns.Queries.GetAllGuns;

internal sealed class GetAllGunsQueryHandler : IQueryHandler<GetAllGunsQuery, List<GunResponse>>
{
    private readonly IGunRepository _repository;

    public GetAllGunsQueryHandler(IGunRepository repository) => _repository = repository;

    public async Task<List<GunResponse>> Handle(GetAllGunsQuery request, CancellationToken cancellationToken) 
        => await _repository.GetAllAsync();
}