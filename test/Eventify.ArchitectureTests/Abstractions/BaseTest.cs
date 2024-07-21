namespace Eventify.ArchitectureTests.Abstractions;

public abstract class BaseTest
{
    protected const string UsersNamespace = "Eventify.Modules.Users";
    protected const string UsersIntegrationEventsNamespace = "Eventify.Modules.Users.IntegrationEvents";

    protected const string EventsNamespace = "Eventify.Modules.Events";
    protected const string EventsIntegrationEventsNamespace = "Eventify.Modules.Events.IntegrationEvents";

    protected const string TicketingNamespace = "Eventify.Modules.Ticketing";
    protected const string TicketingIntegrationEventsNamespace = "Eventify.Modules.Ticketing.IntegrationEvents";

    protected const string AttendanceNamespace = "Eventify.Modules.Attendance";
    protected const string AttendanceIntegrationEventsNamespace = "Eventify.Modules.Attendance.IntegrationEvents";
}
