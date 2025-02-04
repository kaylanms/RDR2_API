namespace RDR2.Domain.Entities;

public class CharacterGun
{
    public Guid CharacterId { get; set; }
    public Character? Character { get; set; }

    public Guid GunId { get; set; }
    public Gun? Gun { get; set; }
}