using MyStoreApi.Domain.Models;

namespace MyStore.xUnit.Fluents
{
    public class PaymentFluent
    {
        public Guid Id { get; set; }
        public Guid CreditCardId { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
        public CreditCard CreditCard { get; set; }
        public decimal Total { get; set; }

        public static PaymentFluent New()
        {
            var order = OrderFluent.New()
                .Build();

            var creditCard = CreditCardFluent.New()
                .Build();

            return new PaymentFluent()
            {
                Id = Guid.NewGuid(),
                CreditCard = creditCard,
                Total = 100.00m
            };
        }

        public Payment Build() =>
            new Payment(CreditCard, OrderId, Total);

        public PaymentFluent WithTotal(decimal total)
        {
            Total = total;
            return this;
        }

        public PaymentFluent WithCreditCardId(Guid id)
        {
            CreditCardId = id;
            return this;
        }

        public PaymentFluent WithCreditCardCvv(CreditCard creditCard)
        {
            CreditCard = creditCard;
            return this;
        }

        public PaymentFluent WithCreditCardNumber(CreditCard creditCard)
        {
            CreditCard = creditCard;
            return this;
        }
    }
}