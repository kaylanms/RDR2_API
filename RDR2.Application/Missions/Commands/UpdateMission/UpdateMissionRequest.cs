namespace RDR2.Application.Missions.Commands.UpdateMission;

public record UpdateMissionRequest(Guid Id, string Name, string Overview, bool IsPrimary);