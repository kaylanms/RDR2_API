namespace RDR2.Application.Missions.Commands.CreateMission;
public record CreateMissionRequest(
    string Name,
    string Overview,
    bool IsPrimary);