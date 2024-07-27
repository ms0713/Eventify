using Eventify.Common.Domain;
using Eventify.Modules.Events.Domain.Events;
using Eventify.Modules.Events.Application.Events.PublishEvent;
using Eventify.Modules.Events.IntegrationTests.Abstractions;
using FluentAssertions;

namespace Eventify.Modules.Events.IntegrationTests.Events;

public class PublishEventTests : BaseIntegrationTest
{
    public PublishEventTests(IntegrationTestWebAppFactory factory)
        : base(factory)
    {
    }

    [Fact]
    public async Task Should_ReturnFailure_WhenEventDoesNotExist()
    {
        // Arrange
        var eventId = Guid.NewGuid();

        var command = new PublishEventCommand(eventId);

        // Act
        Result result = await Sender.Send(command);

        // Assert
        result.Error.Should().Be(EventErrors.NotFound(eventId));
    }

    [Fact]
    public async Task Should_ReturnFailure_WhenEventDoesNotHaveAnyTicketTypes()
    {
        // Arrange
        Guid categoryId = await Sender.CreateCategoryAsync(Faker.Music.Genre());
        Guid eventId = await Sender.CreateEventAsync(categoryId);

        var command = new PublishEventCommand(eventId);

        // Act
        Result result = await Sender.Send(command);

        //Assert
        result.Error.Should().Be(EventErrors.NoTicketsFound);
    }

    [Fact]
    public async Task Should_ReturnSuccess_WhenEventIsPublished()
    {
        // Arrange
        Guid categoryId = await Sender.CreateCategoryAsync(Faker.Music.Genre());
        Guid eventId = await Sender.CreateEventAsync(categoryId);
        await Sender.CreateTicketTypeAsync(eventId);

        var command = new PublishEventCommand(eventId);

        // Act
        Result result = await Sender.Send(command);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }
}
