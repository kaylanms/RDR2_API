using RDR2.Application.Abstractions;
using RDR2.Domain.Models;

namespace RDR2.Application.Missions.Queries.GetMissionById;
public sealed record GetMissionByIdQuery(Guid Id) : IQuery<MissionResponse>;