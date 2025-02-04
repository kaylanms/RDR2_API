using RDR2.Application.Abstractions;
using RDR2.Application.Guns.Queries.GetById;
using RDR2.Domain.Models;
using RDR2.Domain.Repositories;

namespace RDR2.Application.Guns.Queries.GetGunById;

internal sealed class GetGunByIdQueryHandler : IQueryHandler<GetGunByIdQuery, GunResponse>
{
    private readonly IGunRepository _repository;

    public GetGunByIdQueryHandler(IGunRepository repository) => _repository = repository;

    public async Task<GunResponse> Handle(GetGunByIdQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(request.Id);
    }
}
