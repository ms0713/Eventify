using Eventify.Common.Infrastructure.Outbox;
using Eventify.Common.Presentation.Endpoints;
using Eventify.Modules.Events.Application.Abstractions.Data;
using Eventify.Modules.Events.Domain.Categories;
using Eventify.Modules.Events.Domain.Events;
using Eventify.Modules.Events.Domain.TicketTypes;
using Eventify.Modules.Events.Infrastructure.Categories;
using Eventify.Modules.Events.Infrastructure.Database;
using Eventify.Modules.Events.Infrastructure.Events;
using Eventify.Modules.Events.Infrastructure.TicketTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Eventify.Modules.Events.Infrastructure;
public static class EventsModule
{
    public static IServiceCollection AddEventsModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddEndpoints(Presentation.AssemblyReference.Assembly);
        
        services.AddInfrastructure(configuration);

        return services;
    }

    private static void AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        string databaseConnectionString = configuration.GetConnectionString("Database");

        services.AddDbContext<EventsDbContext>((sp, options) =>
        options
        .UseNpgsql(
            databaseConnectionString,
            npgsqlOptions => npgsqlOptions
            .MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Events))
        .UseSnakeCaseNamingConvention()
        .AddInterceptors(sp.GetRequiredService<PublishDomainEventsInterceptor>()));

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<EventsDbContext>());
        
        services.AddScoped<IEventRepository, EventRepository>();
        services.AddScoped<ITicketTypeRepository, TicketTypeRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
    }
}
