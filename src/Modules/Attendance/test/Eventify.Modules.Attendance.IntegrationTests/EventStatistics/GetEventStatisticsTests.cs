using Eventify.Common.Domain;
using Eventify.Modules.Attendance.Application.EventStatistics.GetEventStatistics;
using Eventify.Modules.Attendance.Domain.Events;
using Eventify.Modules.Attendance.IntegrationTests.Abstractions;
using FluentAssertions;

namespace Eventify.Modules.Attendance.IntegrationTests.EventStatistics;

public class GetEventStatisticsTests : BaseIntegrationTest
{
    public GetEventStatisticsTests(IntegrationTestWebAppFactory factory)
        : base(factory)
    {
    }

    [Fact]
    public async Task Should_ReturnFailure_WhenEventStatisticsDoesNotExist()
    {
        // Arrange
        var query = new GetEventStatisticsQuery(Guid.NewGuid());

        // Act
        Result<EventStatisticsResponse> result = await Sender.Send(query);

        // Assert
        result.Error.Should().Be(EventErrors.NotFound(query.EventId));
    }
}
