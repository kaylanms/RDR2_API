using RDR2.Domain.Primitives;

namespace RDR2.Domain.Entities;

public class Gun : Entity
{
    public Gun(Guid id, string name, float damage, int ammunition, int cost) : base(id)
    {
        Name = name;
        Damage = damage;
        Ammunition = ammunition;
        Cost = cost;
        CharacterGuns = [];
    }

    public string Name { get; private set; }
    public float Damage { get; private set; }
    public int Ammunition { get; private set; }
    public int Cost { get; private set; }

    public List<CharacterGun> CharacterGuns { get; private set; }
}