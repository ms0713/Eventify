using Eventify.Common.Application.Messaging;
using Eventify.Modules.Ticketing.Application.Abstractions.Payments;
using Eventify.Modules.Ticketing.Domain.Payments;

namespace Eventify.Modules.Ticketing.Application.Payments.RefundPayment;

internal sealed class PaymentPartiallyRefundedDomainEventHandler(IPaymentService paymentService)
    : DomainEventHandler<PaymentPartiallyRefundedDomainEvent>
{
    public override async Task Handle(
        PaymentPartiallyRefundedDomainEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        await paymentService.RefundAsync(domainEvent.TransactionId, domainEvent.RefundAmount);
    }
}
