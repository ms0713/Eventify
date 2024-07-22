using Eventify.Modules.Attendance.Infrastructure.Database;
using Eventify.Modules.Events.Infrastructure.Database;
using Eventify.Modules.Ticketing.Infrastructure.Database;
using Eventify.Modules.Users.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Eventify.Api.Extentions;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        ApplyMigration<EventsDbContext>(scope);
        ApplyMigration<UsersDbContext>(scope);
        ApplyMigration<TicketingDbContext>(scope);
        ApplyMigration<AttendanceDbContext>(scope);
    }

    private static void ApplyMigration<TDbContext>(IServiceScope scope)
        where TDbContext : DbContext
    {
        using TDbContext context = scope.ServiceProvider.GetRequiredService<TDbContext>();

        context.Database.Migrate();
    }
}
