using Eventify.Common.Application.EventBus;
using Eventify.Modules.Events.ArchitectureTests.Abstractions;
using NetArchTest.Rules;

namespace Eventify.Modules.Events.ArchitectureTests.Presentation;

public class PresentationTests : BaseTest
{
    [Fact]
    public void IntegrationEventConsumer_Should_NotBePublic()
    {
        Types.InAssembly(PresentationAssembly)
            .That()
            .ImplementInterface(typeof(IIntegrationEventHandler<>))
            .Or()
            .Inherit(typeof(IntegrationEventHandler<>))
            .Should()
            .NotBePublic()
            .GetResult()
            .ShouldBeSuccessful();
    }


    [Fact]
    public void IntegrationEventConsumer_Should_BeSealed()
    {
        Types.InAssembly(PresentationAssembly)
            .That()
            .ImplementInterface(typeof(IIntegrationEventHandler<>))
            .Should()
            .BeSealed()
            .GetResult()
            .ShouldBeSuccessful();
    }

    [Fact]
    public void IntegrationEventConsumer_ShouldHave_NameEndingWith_IntegrationEventConsumer()
    {
        Types.InAssembly(PresentationAssembly)
            .That()
            .ImplementInterface(typeof(IIntegrationEventHandler<>))
            .Should()
            .HaveNameEndingWith("IntegrationEventHandler")
            .GetResult()
            .ShouldBeSuccessful();
    }
}
