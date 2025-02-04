using RDR2.Domain.Primitives;

namespace RDR2.Domain.Entities;

public class Mission : Entity
{
    public Mission(Guid id, string name, string overview, bool isPrimary) : base(id)
    {
        Name = name;
        Overview = overview;
        IsPrimary = isPrimary;
    }

    public string Name { get; private set; }
    public string Overview { get; private set; }
    public bool IsPrimary { get; private set; }
}