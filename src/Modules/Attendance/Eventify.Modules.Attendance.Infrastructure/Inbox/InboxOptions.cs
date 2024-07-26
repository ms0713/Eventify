namespace Eventify.Modules.Attendance.Infrastructure.Inbox;

internal sealed class InboxOptions
{
    public int IntervalInSeconds { get; init; }

    public int BatchSize { get; init; }
}
