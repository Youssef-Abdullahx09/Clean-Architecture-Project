using Domain.Enums;
using Domain.Primitives;

namespace Domain.Entities;

public sealed class Gathering : Entity
{
    private Gathering(
        Guid id,
        string name,
        string? location,
        Member creator,
        GatheringType type,
        DateTime scheduledAtUtc)
        : base(id)
    {
        Name = name;
        Location = location;
        Creator = creator;
        Type = type;
        ScheduledAtUtc = scheduledAtUtc;
        NumberOfAttendees = 0;
        _invitations = new List<Invitation>();
        _attendees = new List<Attendee>();
    }
    public string Name { get; private set; }
    public string Location { get; private set; }
    public GatheringType Type { get; private set; }
    public int? InvitationsValidBeforeInHours { get; private set; }
    public int? MaximumNumberOfAttendees { get; private set; }
    public int NumberOfAttendees { get; private set; }
    public DateTime ScheduledAtUtc { get; private set; }
    public Guid CreatorId { get; private set; }
    public Member Creator { get; private set; }
    public IReadOnlyCollection<Invitation> Invitations => _invitations;
    public IReadOnlyCollection<Attendee> Attendees => _attendees;

    private readonly List<Invitation> _invitations;
    private readonly List<Attendee> _attendees;

    public static Gathering Create(
        Guid id,
        string name,
        string? location,
        Member creator,
        GatheringType type,
        DateTime scheduledAtUtc,
        int? maximumNumberOfAttendees,
        int? invitationsValidBeforeInHours)
    {
        var gathering = new Gathering(id, name, location, creator, type, scheduledAtUtc);
        switch (gathering.Type)
        {
            case GatheringType.WithFixedNumberOfAttendees:
                if (maximumNumberOfAttendees is null)
                {
                    throw new Exception(
                        $"{nameof(maximumNumberOfAttendees)} can't be null.");
                }

                gathering.MaximumNumberOfAttendees = maximumNumberOfAttendees;
                break;
            case GatheringType.WithExpirationForInvitations:
                if (invitationsValidBeforeInHours is null)
                {
                    throw new Exception(
                        $"{nameof(invitationsValidBeforeInHours)} can't be null.");
                }

                gathering.InvitationsValidBeforeInHours = invitationsValidBeforeInHours;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(GatheringType));
        }

        return gathering;
    }

    public Invitation SendInvitation(Member member)
    {
        if (Creator.Id == member.Id)
            throw new Exception("Cant send invitation to the gathering creator.");

        if (ScheduledAtUtc < DateTime.UtcNow)
            throw new Exception("Cant send invitation for gathering in the past.");

        var invitation = new Invitation(Guid.NewGuid(), member, this);

        _invitations.Add(invitation);

        return invitation;
    }

    public Attendee? AcceptInvitation(Invitation invitation)
    {
        var expired = (Type == GatheringType.WithFixedNumberOfAttendees
                && NumberOfAttendees == MaximumNumberOfAttendees
            || Type == GatheringType.WithExpirationForInvitations
                && ScheduledAtUtc < DateTime.UtcNow);

        if (expired)
        {
            invitation.Expire();
            return null;
        }

        var attendee = invitation.Accept();

        _attendees.Add(attendee);
        NumberOfAttendees++;

        return attendee;
    }
}
