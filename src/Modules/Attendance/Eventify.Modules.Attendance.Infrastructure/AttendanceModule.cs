using Eventify.Common.Infrastructure.Outbox;
using Eventify.Common.Presentation.Endpoints;
using Eventify.Modules.Attendance.Application.Abstractions.Authentication;
using Eventify.Modules.Attendance.Application.Abstractions.Data;
using Eventify.Modules.Attendance.Domain.Attendees;
using Eventify.Modules.Attendance.Domain.Events;
using Eventify.Modules.Attendance.Domain.Tickets;
using Eventify.Modules.Attendance.Infrastructure.Attendees;
using Eventify.Modules.Attendance.Infrastructure.Authentication;
using Eventify.Modules.Attendance.Infrastructure.Database;
using Eventify.Modules.Attendance.Infrastructure.Events;
using Eventify.Modules.Attendance.Infrastructure.Tickets;
using Eventify.Modules.Attendance.Presentation.Attendees;
using Eventify.Modules.Attendance.Presentation.Events;
using Eventify.Modules.Attendance.Presentation.Tickets;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Eventify.Modules.Attendance.Infrastructure;

public static class AttendanceModule
{
    public static IServiceCollection AddAttendanceModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);

        services.AddEndpoints(Presentation.AssemblyReference.Assembly);

        return services;
    }

    public static void ConfigureConsumers(IRegistrationConfigurator registrationConfigurator)
    {
        registrationConfigurator.AddConsumer<UserRegisteredIntegrationEventConsumer>();
        registrationConfigurator.AddConsumer<UserProfileUpdatedIntegrationEventConsumer>();
        registrationConfigurator.AddConsumer<EventPublishedIntegrationEventConsumer>();
        registrationConfigurator.AddConsumer<TicketIssuedIntegrationEventConsumer>();
    }

    private static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AttendanceDbContext>((sp, options) =>
            options
                .UseNpgsql(
                    configuration.GetConnectionString("Database"),
                    npgsqlOptions => npgsqlOptions
                        .MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Attendance))
                .UseSnakeCaseNamingConvention()
                .AddInterceptors(sp.GetRequiredService<PublishDomainEventsInterceptor>()));

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<AttendanceDbContext>());

        services.AddScoped<IAttendeeRepository, AttendeeRepository>();
        services.AddScoped<IEventRepository, EventRepository>();
        services.AddScoped<ITicketRepository, TicketRepository>();

        services.AddScoped<IAttendanceContext, AttendanceContext>();
    }
}
