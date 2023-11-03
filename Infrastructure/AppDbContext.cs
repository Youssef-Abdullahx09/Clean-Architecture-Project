using Domain.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;
public sealed class AppDbContext : DbContext, IAppDbContext
{
    public AppDbContext(DbContextOptions options)
        : base(options) { }

    public DbSet<Member> Members { get; set; }
    public DbSet<Invitation> Invitations { get; set; }
    public DbSet<Gathering> Gatherings { get; set; }
    public DbSet<Attendee> Attendees { get; set; }

    protected override void OnModelCreating(ModelBuilder builder) =>
        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
}
