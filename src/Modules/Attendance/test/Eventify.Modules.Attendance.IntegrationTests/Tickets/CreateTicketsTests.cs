﻿using Eventify.Common.Domain;
using Eventify.Modules.Attendance.Application.Tickets.CreateTicket;
using Eventify.Modules.Attendance.Domain.Attendees;
using Eventify.Modules.Attendance.Domain.Events;
using Eventify.Modules.Attendance.IntegrationTests.Abstractions;
using FluentAssertions;

namespace Eventify.Modules.Attendance.IntegrationTests.Tickets;

public class CreateTicketsTests : BaseIntegrationTest
{
    public CreateTicketsTests(IntegrationTestWebAppFactory factory)
       : base(factory)
    {
    }

    [Fact]
    public async Task Should_ReturnFailure_WhenAttendeeDoesNotExist()
    {
        // Arrange
        var command = new CreateTicketCommand(
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Faker.Random.String());

        // Act
        Result result = await Sender.Send(command);

        // Assert
        result.Error.Should().Be(AttendeeErrors.NotFound(command.AttendeeId));
    }

    [Fact]
    public async Task Should_ReturnFailure_WhenEventDoesNotExist()
    {
        // Arrange
        Guid attendeeId = await Sender.CreateAttendeeAsync(Guid.NewGuid());

        var command = new CreateTicketCommand(
            Guid.NewGuid(),
            attendeeId,
            Guid.NewGuid(),
            Faker.Random.String());

        // Act
        Result result = await Sender.Send(command);

        // Assert
        result.Error.Should().Be(EventErrors.NotFound(command.EventId));
    }

    [Fact]
    public async Task Should_ReturnSuccess_WhenTicketIsCreated()
    {
        //Arrange
        Guid attendeeId = await Sender.CreateAttendeeAsync(Guid.NewGuid());
        Guid eventId = await Sender.CreateEventAsync(Guid.NewGuid());

        var command = new CreateTicketCommand(
            Guid.NewGuid(),
            attendeeId,
            eventId,
            Ulid.NewUlid().ToString());

        //Act
        Result result = await Sender.Send(command);

        //Assert
        result.IsSuccess.Should().BeTrue();
    }
}
