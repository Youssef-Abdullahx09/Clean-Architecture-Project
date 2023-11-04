namespace Domain.Entities;

public sealed class Attendee
{
    internal Attendee(Invitation invitation)
    {
        MemberId = invitation.MemberId;
        GatheringId = invitation.GatheringId;
    }
    public Guid MemberId { get; set; }
    public Member Member { get; set; }
    public Guid GatheringId { get; set; }
    public Gathering Gathering { get; set; }
}
