using Eventify.Common.Application.Messaging;

namespace Eventify.Modules.Attendance.Application.EventStatistics.GetEventStatistics;

public sealed record GetEventStatisticsQuery(Guid EventId) : IQuery<EventStatisticsResponse>;
