namespace Domain.Entities;

public sealed class Attendee
{
    public Guid MemberId { get; set; }
    public Member Member { get; set; }
    public Guid GatheringId { get; set; }
    public Gathering Gathering { get; set; }
}
