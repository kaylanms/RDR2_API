using RDR2.Domain.Primitives;

namespace RDR2.Domain.Entities;

public class Character : Entity
{
    public Character(Guid id, string firstName, string lastName, int age, bool isAlive) : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        Age = age;
        IsAlive = isAlive;
        CharacterGuns = [];
    }

    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public int Age { get; private set; }
    public bool IsAlive { get; private set; }
    public Gang? Gang { get; private set; }
    public Guid? GangId { get; private set; }

    public List<CharacterGun> CharacterGuns { get; private set; }

    public void MarkAsDead()
    {
        IsAlive = false;
        LeaveGang();
    }

    public void JoinGang(Gang gang)
    {
        Gang = gang;
        GangId = gang.Id;
        gang.AddCharacter(this);
    }

    public void LeaveGang()
    {
        Gang = null;
        GangId = null;
    }
}