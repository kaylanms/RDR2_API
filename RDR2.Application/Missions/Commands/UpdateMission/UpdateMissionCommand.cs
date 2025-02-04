using RDR2.Application.Abstractions;
using RDR2.Domain.Models;

namespace RDR2.Application.Missions.Commands.UpdateMission;

public sealed record UpdateMissionCommand(Guid Id, string Name, string Overview, bool IsPrimary) : ICommand<MissionResponse>;