using Domain.Enums;
using Domain.Primitives;

namespace Domain.Entities
{
    public class Invitation : Entity
    {
        internal Invitation(Guid id, Member member, Gathering gathering)
            : base(id)
        {
            Member = member;
            Gathering = gathering;
            Status = InvitationStatus.Pending;
            CreatedOnUtc = DateTime.UtcNow;
        }
        public InvitationStatus Status { get; private set; }
        public Guid MemberId { get; private set; }
        public Member Member { get; private set; }
        public Gathering Gathering { get; private set; }
        public Guid GatheringId { get; private set; }
        public DateTime CreatedOnUtc { get; private set; }
        public DateTime? ModifiedOnUtc { get; private set; }

        internal void Expire()
        {
            Status = InvitationStatus.Expired;
            ModifiedOnUtc = DateTime.UtcNow;
        }

        internal Attendee Accept()
        {
            Status = InvitationStatus.Accepted;
            ModifiedOnUtc = DateTime.UtcNow;

            var attendee = new Attendee(this);

            return attendee;
        }
    }
}
