using RDR2.Application.Abstractions;
using RDR2.Domain.Models;

namespace RDR2.Application.Characters.Queries.GetAllCharacters;

public sealed record GetAllCharactersQuery() : IQuery<List<CharacterResponse>>;