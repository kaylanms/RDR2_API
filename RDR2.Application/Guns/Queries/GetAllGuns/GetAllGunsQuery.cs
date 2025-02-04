using RDR2.Application.Abstractions;
using RDR2.Domain.Models;

namespace RDR2.Application.Guns.Queries.GetAllGuns;

public record GetAllGunsQuery() : IQuery<List<GunResponse>>;