using Eventify.Common.Application.Messaging;

namespace Eventify.Modules.Attendance.Application.Attendees.GetAttendee;

public sealed record GetAttendeeQuery(Guid CustomerId) : IQuery<AttendeeResponse>;
