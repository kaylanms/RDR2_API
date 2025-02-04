namespace RDR2.Domain.Primitives;

public abstract class Entity
{
    protected Entity(Guid id) => Id = id;

    // create parameterless constructor 
    public Guid Id { get; protected set; }
}