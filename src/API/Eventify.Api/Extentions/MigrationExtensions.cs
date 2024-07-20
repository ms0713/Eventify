using Eventify.Modules.Events.Infrastructure.Database;
using Eventify.Modules.Ticketing.Infrastructure.Database;
using Eventify.Modules.Users.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Eventify.Api.Extentions;

internal static class MigrationExtensions
{
    internal static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        ApplyMigration<EventsDbContext>(scope);
        ApplyMigration<UsersDbContext>(scope);
        ApplyMigration<TicketingDbContext>(scope);
    }

    private static void ApplyMigration<TDbContext>(IServiceScope scope)
        where TDbContext : DbContext
    {
        using TDbContext context = scope.ServiceProvider.GetRequiredService<TDbContext>();

        context.Database.Migrate();
    }
}
