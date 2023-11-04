using Domain.Abstractions;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Invitations.Commands.AcceptInvitationCommand;

public sealed class AcceptInvitationCommandHandler : IRequestHandler<AcceptInvitationCommand, Unit>
{
    private readonly IAppDbContext _context;
    public AcceptInvitationCommandHandler(IAppDbContext context)
    {
        _context = context;
    }
    public async Task<Unit> Handle(AcceptInvitationCommand request, CancellationToken cancellationToken)
    {
        var invitation = await _context.Invitations.Where(x => x.Id == request.InvitationId)
            .Include(x => x.Member)
            .Include(x => x.Gathering)
            .FirstOrDefaultAsync(cancellationToken);

        if (invitation is null
            || invitation.Member is null
            || invitation.Gathering is null
            || invitation.Status != InvitationStatus.Pending)
            return Unit.Value;

        var attendee = invitation.Gathering.AcceptInvitation(invitation);

        if (attendee is not null)
            await _context.Attendees.AddAsync(attendee, cancellationToken);

        await _context.SaveChangesAsync();

        //send invitation accept email

        return Unit.Value;
    }
}
