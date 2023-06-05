using MyStoreApi.Domain.Models;

namespace MyStoreApi.Contracts.PaymentDto
{
    public class PaymentLoad
    {
        public Guid OrderId { get; set; }
        public PaymentStatus Status { get; set; }

        public PaymentLoad() {}
    }
}