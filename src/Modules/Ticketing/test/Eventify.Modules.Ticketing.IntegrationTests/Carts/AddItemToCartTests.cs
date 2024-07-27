using Eventify.Common.Domain;
using Eventify.Modules.Ticketing.Application.Carts.AddItemToCart;
using Eventify.Modules.Ticketing.Domain.Customers;
using Eventify.Modules.Ticketing.Domain.Events;
using Eventify.Modules.Ticketing.IntegrationTests.Abstractions;
using FluentAssertions;

namespace Eventify.Modules.Ticketing.IntegrationTests.Carts;

public class AddItemToCartTests : BaseIntegrationTest
{
    private const decimal Quantity = 10;

    public AddItemToCartTests(IntegrationTestWebAppFactory factory)
        : base(factory)
    {
    }

    [Fact]
    public async Task Should_ReturnFailure_WhenCustomerDoesNotExist()
    {
        //Arrange
        var command = new AddItemToCartCommand(
            Guid.NewGuid(),
            Guid.NewGuid(),
            Faker.Random.Decimal());

        //Act
        Result result = await Sender.Send(command);

        //Assert
        result.Error.Should().Be(CustomerErrors.NotFound(command.CustomerId));
    }

    [Fact]
    public async Task Should_ReturnFailure_WhenTicketTypeDoesNotExist()
    {
        //Arrange
        Guid customerId = await Sender.CreateCustomerAsync(Guid.NewGuid());

        var command = new AddItemToCartCommand(
            customerId,
            Guid.NewGuid(),
            Faker.Random.Decimal());

        //Act
        Result result = await Sender.Send(command);

        //Assert
        result.Error.Should().Be(TicketTypeErrors.NotFound(command.TicketTypeId));
    }

    [Fact]
    public async Task Should_ReturnFailure_WhenNotEnoughQuantity()
    {
        //Arrange
        Guid customerId = await Sender.CreateCustomerAsync(Guid.NewGuid());
        var eventId = Guid.NewGuid();
        var ticketTypeId = Guid.NewGuid();

        await Sender.CreateEventWithTicketTypeAsync(eventId, ticketTypeId, Quantity);


        var command = new AddItemToCartCommand(
            customerId,
            ticketTypeId,
            Quantity + 1);

        //Act
        Result result = await Sender.Send(command);

        //Assert
        result.Error.Should().Be(TicketTypeErrors.NotEnoughQuantity(Quantity));
    }

    [Fact]
    public async Task Should_ReturnSuccess_WhenItemAddedToCart()
    {
        //Arrange
        Guid customerId = await Sender.CreateCustomerAsync(Guid.NewGuid());
        var eventId = Guid.NewGuid();
        var ticketTypeId = Guid.NewGuid();

        await Sender.CreateEventWithTicketTypeAsync(eventId, ticketTypeId, Quantity);

        var command = new AddItemToCartCommand(
            customerId,
            ticketTypeId,
            Quantity);

        //Act
        Result result = await Sender.Send(command);

        //Assert
        result.IsSuccess.Should().BeTrue();
    }
}
