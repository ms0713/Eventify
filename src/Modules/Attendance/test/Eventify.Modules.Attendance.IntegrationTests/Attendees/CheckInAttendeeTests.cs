﻿using Eventify.Common.Domain;
using Eventify.Modules.Attendance.Application.Attendees.CheckInAttendee;
using Eventify.Modules.Attendance.Domain.Attendees;
using Eventify.Modules.Attendance.Domain.Tickets;
using Eventify.Modules.Attendance.IntegrationTests.Abstractions;
using FluentAssertions;

namespace Eventify.Modules.Attendance.IntegrationTests.Attendees;

public class CheckInAttendeeTests : BaseIntegrationTest
{
    public CheckInAttendeeTests(IntegrationTestWebAppFactory factory)
       : base(factory)
    {
    }

    [Fact]
    public async Task Should_ReturnFailure_WhenAttendeeDoesNotExist()
    {
        // Arrange
        var command = new CheckInAttendeeCommand(
            Guid.NewGuid(),
            Guid.NewGuid());

        // Act
        Result result = await Sender.Send(command);

        // Assert
        result.Error.Should().Be(AttendeeErrors.NotFound(command.AttendeeId));
    }

    [Fact]
    public async Task Should_ReturnFailure_WhenTicketDoesNotExist()
    {
        // Arrange
        Guid attendeeId = await Sender.CreateAttendeeAsync(Guid.NewGuid());

        var command = new CheckInAttendeeCommand(
            attendeeId,
            Guid.NewGuid());

        // Act
        Result result = await Sender.Send(command);

        // Assert
        result.Error.Should().Be(TicketErrors.NotFound);
    }

    [Fact]
    public async Task Should_ReturnSuccess_WhenAttendeeCheckedIn()
    {
        //Arrange
        Guid attendeeId = await Sender.CreateAttendeeAsync(Guid.NewGuid());
        Guid eventId = await Sender.CreateEventAsync(Guid.NewGuid());
        Guid ticketId = await Sender.CreateTicketAsync(Guid.NewGuid(), attendeeId, eventId);

        var command = new CheckInAttendeeCommand(
            attendeeId,
            ticketId);

        //Act
        Result result = await Sender.Send(command);

        //Assert
        result.IsSuccess.Should().BeTrue();
    }
}
