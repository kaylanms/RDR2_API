using RDR2.Application.Abstractions;
using RDR2.Domain.Models;
using RDR2.Domain.Repositories;

namespace RDR2.Application.Characters.Queries.GetCharacterById;

internal sealed class GetCharacterByIdQueryHandler : IQueryHandler<GetCharacterByIdQuery, CharacterResponse>
{
    private readonly ICharacterRepository _repository;

    public GetCharacterByIdQueryHandler(ICharacterRepository repository)
    {
        _repository = repository;
    }

    public async Task<CharacterResponse> Handle(GetCharacterByIdQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(request.Id);
    }
}
