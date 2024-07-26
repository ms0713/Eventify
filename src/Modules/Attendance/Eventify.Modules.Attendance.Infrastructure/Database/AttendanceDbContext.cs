using Eventify.Common.Infrastructure.Inbox;
using Eventify.Common.Infrastructure.Outbox;
using Eventify.Modules.Attendance.Application.Abstractions.Data;
using Eventify.Modules.Attendance.Domain.Attendees;
using Eventify.Modules.Attendance.Domain.Events;
using Eventify.Modules.Attendance.Domain.Tickets;
using Eventify.Modules.Attendance.Infrastructure.Attendees;
using Eventify.Modules.Attendance.Infrastructure.Events;
using Eventify.Modules.Attendance.Infrastructure.Tickets;
using Microsoft.EntityFrameworkCore;

namespace Eventify.Modules.Attendance.Infrastructure.Database;

public sealed class AttendanceDbContext(DbContextOptions<AttendanceDbContext> options)
    : DbContext(options), IUnitOfWork
{
    internal DbSet<Attendee> Attendees { get; set; }

    internal DbSet<Event> Events { get; set; }

    internal DbSet<Ticket> Tickets { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Attendance);

        modelBuilder.ApplyConfiguration(new OutboxMessageConfiguration());
        modelBuilder.ApplyConfiguration(new OutboxMessageConsumerConfiguration());
        modelBuilder.ApplyConfiguration(new InboxMessageConfiguration());
        modelBuilder.ApplyConfiguration(new InboxMessageConsumerConfiguration());
        modelBuilder.ApplyConfiguration(new AttendeeConfiguration());
        modelBuilder.ApplyConfiguration(new EventConfiguration());
        modelBuilder.ApplyConfiguration(new TicketConfiguration());
    }
}
