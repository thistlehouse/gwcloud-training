using MyStoreApi.Domain.Models;

namespace MyStoreApi.Contracts.PaymentDto
{
    public class PaymentRequest
    {
        public Guid OrderId { get; set; }
        public CreditCard CreditCard { get; set; }
        public decimal Total { get; set; }

        public PaymentRequest(CreditCard creditCard,
            decimal total)
        {
            CreditCard = creditCard;
            Total = total;
        }
    }
}