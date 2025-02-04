using RDR2.Application.Abstractions;
using RDR2.Domain.Models;

namespace RDR2.Application.Guns.Queries.GetById;

public record GetGunByIdQuery(Guid Id) : IQuery<GunResponse>;