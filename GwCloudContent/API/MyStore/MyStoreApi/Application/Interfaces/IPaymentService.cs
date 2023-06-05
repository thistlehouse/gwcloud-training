using MyStoreApi.Domain.Models;

namespace MyStoreApi.Application.Interfaces
{
    public interface IPaymentService
    {
        void ProcessPayment(Payment payment);
        PaymentStatus CheckPaymentStatus();
    }
}