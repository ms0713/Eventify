using Eventify.Common.Domain;
using Eventify.Modules.Attendance.Domain.Events;
using Eventify.Modules.Attendance.UnitTests.Abstractions;
using FluentAssertions;

namespace Eventify.Modules.Attendance.UnitTests.Events;

public class EventTests : BaseTest
{
    [Fact]
    public void Should_RaiseDomainEvent_WhenEventCreated()
    {
        //Arrange
        var eventId = Guid.NewGuid();
        DateTime startsAtUtc = DateTime.Now;

        //Act
        Result<Event> result = Event.Create(
            eventId,
            Faker.Music.Genre(),
            Faker.Music.Genre(),
            Faker.Address.StreetAddress(),
            startsAtUtc,
            null);

        //Assert
        EventCreatedDomainEvent domainEvent =
            AssertDomainEventWasPublished<EventCreatedDomainEvent>(result.Value);

        domainEvent.EventId.Should().Be(result.Value.Id);
    }
}
