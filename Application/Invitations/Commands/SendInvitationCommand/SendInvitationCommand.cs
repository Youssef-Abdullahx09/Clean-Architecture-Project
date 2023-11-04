using MediatR;

namespace Application.Invitations.Commands.SendInvitationCommand;

public sealed record SendInvitationCommand(
    Guid MemberId,
    Guid GatheringId) : IRequest<Unit>;