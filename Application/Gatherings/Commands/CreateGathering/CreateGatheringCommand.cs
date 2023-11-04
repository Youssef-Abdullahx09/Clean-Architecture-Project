using Domain.Enums;
using MediatR;

namespace Application.Gatherings.Commands.CreateGathering;

public sealed record CreateGatheringCommand(
        Guid MemberId,
        string Name,
        string? Location,
        GatheringType Type,
        DateTime ScheduledAtUtc,
        int? MaximumNumberOfAttendees,
        int? InvitationsValidBeforeHours) : IRequest<Unit>;
