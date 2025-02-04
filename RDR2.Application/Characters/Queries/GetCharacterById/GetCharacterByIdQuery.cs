using RDR2.Application.Abstractions;
using RDR2.Domain.Models;

namespace RDR2.Application.Characters.Queries.GetCharacterById;

public sealed record GetCharacterByIdQuery(Guid Id) : IQuery<CharacterResponse>;