using RDR2.Application.Abstractions;
using RDR2.Domain.Models;

namespace RDR2.Application.Gangs.Queries.GetAllGangs;

public sealed record GetAllGangsQuery() : IQuery<List<GangResponse>>;