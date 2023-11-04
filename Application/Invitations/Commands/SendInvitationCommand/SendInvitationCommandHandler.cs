using Domain.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Invitations.Commands.SendInvitationCommand;

public class SendInvitationCommandHandler : IRequestHandler<SendInvitationCommand, Unit>
{
    private readonly IAppDbContext _context;
    public SendInvitationCommandHandler(IAppDbContext context)
    {
        _context = context;
    }
    public async Task<Unit> Handle(SendInvitationCommand request, CancellationToken cancellationToken)
    {
        var member = await _context.Members.Where(x => x.Id == request.MemberId)
            .FirstOrDefaultAsync(cancellationToken);

        var gathering = await _context.Gatherings.Where(x => x.Id == request.GatheringId)
            .FirstOrDefaultAsync(cancellationToken);

        if (member is null || gathering is null)
            return Unit.Value;

        var invitation = gathering.SendInvitation(member);

        await _context.Invitations.AddAsync(invitation);
        await _context.SaveChangesAsync();

        //Send email to member
        return Unit.Value;
    }
}
