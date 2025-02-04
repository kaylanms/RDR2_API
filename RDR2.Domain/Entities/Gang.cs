using RDR2.Domain.Primitives;

namespace RDR2.Domain.Entities;

public class Gang : Entity
{
    public Gang(Guid id, string name, Guid leaderId) : base(id)
    {
        Name = name;
        LeaderId = leaderId;
        Characters = [];
        TotalMembers = 0;
    }

    public string Name { get; private set; }
    public int TotalMembers { get; private set; }
    public List<Character> Characters { get; private set; }
    public Character? Leader { get; private set; }
    public Guid LeaderId { get; private set; }


    public void AddCharacter(Character character)
    {
        Characters.Add(character);
        TotalMembers += 1;
    }

    public void RemoveCharacter(Character character)
    {
        Characters.Remove(character);
        TotalMembers -= 1;
    }
}