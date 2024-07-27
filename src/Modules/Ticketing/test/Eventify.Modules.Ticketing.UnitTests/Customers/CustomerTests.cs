using Eventify.Common.Domain;
using Eventify.Modules.Ticketing.Domain.Customers;
using Eventify.Modules.Ticketing.UnitTests.Abstractions;
using FluentAssertions;

namespace Eventify.Modules.Ticketing.UnitTests.Customers;

public class CustomerTests : BaseTest
{
    [Fact]
    public void Create_ShouldReturnValue_WhenCustomerIsCreated()
    {
        //Act
        Result<Customer> result = Customer.Create(
            Guid.NewGuid(), 
            Faker.Internet.Email(),
            Faker.Name.FirstName(),
            Faker.Name.LastName());
        //Assert
        result.Value.Should().NotBeNull();
    }
}
