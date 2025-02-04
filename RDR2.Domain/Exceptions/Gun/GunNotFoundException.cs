namespace RDR2.Domain.Exceptions.Gun;

public class GunNotFoundException : Exception
{
    public GunNotFoundException(Guid gunId) 
        : base($"Gun with ID {gunId} not found.")
    {
    }
}