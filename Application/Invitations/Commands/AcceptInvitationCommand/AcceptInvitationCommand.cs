using MediatR;

namespace Application.Invitations.Commands.AcceptInvitationCommand;

public sealed record AcceptInvitationCommand(Guid InvitationId) : IRequest<Unit>;
