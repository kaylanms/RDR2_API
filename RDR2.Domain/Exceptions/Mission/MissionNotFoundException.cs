namespace RDR2.Domain.Exceptions.Mission;

public sealed class MissionNotFoundException : Exception
{
    public MissionNotFoundException(Guid missionId)
        : base($"Mission with ID {missionId} not found.")
    {

    }
}