using RDR2.Application.Abstractions;
using RDR2.Domain.Models;
using RDR2.Domain.Repositories;

namespace RDR2.Application.Characters.Queries.GetAllCharacters;

internal sealed class GetAllCharactersQueryHandler : IQueryHandler<GetAllCharactersQuery, List<CharacterResponse>>
{
    private readonly ICharacterRepository _repository;

    public GetAllCharactersQueryHandler(ICharacterRepository repository) => _repository = repository;
    public async Task<List<CharacterResponse>> Handle(GetAllCharactersQuery request, CancellationToken cancellationToken) 
        => await _repository.GetAllAsync();
}