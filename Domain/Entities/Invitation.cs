namespace Domain.Entities
{
    public class Invitation
    {
        public Guid Id { get; set; }
        public Guid MemberId { get; set; }
        public Member Member { get; set; }
        public Guid GatheringId { get; set; }
        public Gathering Gathering { get; set; }
        public string Status { get; set; }
    }
}
