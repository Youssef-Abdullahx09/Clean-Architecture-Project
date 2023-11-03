using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domain.Abstractions
{
    public interface IAppDbContext
    {
        DbSet<Member> Members { get; set; }
        DbSet<Invitation> Invitations { get; set; }
        DbSet<Gathering> Gatherings { get; set; }
        DbSet<Attendee> Attendees { get; set; }
    }
}
