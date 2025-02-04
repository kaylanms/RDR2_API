using RDR2.Application.Abstractions;
using RDR2.Domain.Models;

namespace RDR2.Application.Missions.Queries.GetAllMissions;
public sealed record GetAllMissionsQuery() : IQuery<List<MissionResponse>>;