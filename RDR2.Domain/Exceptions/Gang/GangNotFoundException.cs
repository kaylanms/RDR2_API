namespace RDR2.Domain.Exceptions.Gang;

public sealed class GangNotFoundException : Exception
{
    public GangNotFoundException(Guid id)
        : base($"Gang with ID {id} not found.")
    {
    }
}