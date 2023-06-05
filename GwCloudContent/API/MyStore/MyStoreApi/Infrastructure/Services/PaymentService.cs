using MyStoreApi.Application.Interfaces;
using MyStoreApi.Domain.Models;
using MyStoreApi.Infrastructure.Services.MessageBroker;

namespace MyStoreApi.Infrastructure.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IMessageProducer _messageProducer;

        public PaymentService(IMessageProducer messageProducer)
        {
            _messageProducer = messageProducer;
        }

        public void ProcessPayment(Payment payment)
        {
            _messageProducer.Publisher<Payment>(payment, "payment" );
        }

        public PaymentStatus CheckPaymentStatus()
        {
            var status = _messageProducer.Consumer<PaymentStatus>("payment");

            return status;
        }
    }
}