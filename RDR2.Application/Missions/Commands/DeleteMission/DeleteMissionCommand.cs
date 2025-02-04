using RDR2.Application.Abstractions;

namespace RDR2.Application.Missions.Commands.DeleteMission;
public record DeleteMissionCommand(
    Guid Id) : ICommand<Guid>;