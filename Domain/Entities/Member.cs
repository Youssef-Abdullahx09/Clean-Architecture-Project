using Domain.Primitives;

namespace Domain.Entities;
public sealed class Member : Entity
{
    public Member(Guid id, string firstName, string lastName)
        : base(id)
    {
        FirstName = firstName;
        LasttName = lastName;
    }
    public string FirstName { get; set; }
    public string LasttName { get; set; }
    public string Email { get; set; }
}
