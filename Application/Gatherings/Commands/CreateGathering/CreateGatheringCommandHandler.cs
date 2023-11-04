using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Gatherings.Commands.CreateGathering
{
    public sealed class CreateGatheringCommandHandler : IRequestHandler<CreateGatheringCommand, Unit>
    {
        private readonly IAppDbContext _context;
        public CreateGatheringCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreateGatheringCommand request, CancellationToken cancellationToken)
        {
            var member = await _context.Members.Where(x => x.Id == request.MemberId)
                .FirstOrDefaultAsync(cancellationToken);

            if (member is null)
                return Unit.Value;

            var gathering = Gathering.Create(
                Guid.NewGuid(),
                request.Name,
                request.Location,
                member,
                request.Type,
                request.ScheduledAtUtc,
                request.MaximumNumberOfAttendees,
                request.InvitationsValidBeforeHours);

            await _context.Gatherings.AddAsync(gathering);
            await _context.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
