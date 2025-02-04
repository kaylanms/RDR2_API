using RDR2.Application.Abstractions;

namespace RDR2.Application.Missions.Commands.CreateMission;
public record CreateMissionCommand(
    string Name,
    string Overview,
    bool IsPrimary) : ICommand<Guid>;