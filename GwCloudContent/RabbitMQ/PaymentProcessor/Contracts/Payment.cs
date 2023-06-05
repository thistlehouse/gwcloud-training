namespace PaymentProcessor.Microservice.Broker
{
    public class Payment
    {
        public CreditCard CreditCard { get; set; }
        public Guid OrderId { get; set; }
        public PaymentStatus Status { get; set; } =  PaymentStatus.Pending;
        public decimal Total { get; set; }
        public Payment() {}

        public Payment(CreditCard creditCard,
            Guid orderId,
            decimal total)
        {
            CreditCard = creditCard;
            OrderId = orderId;
            Status = PaymentStatus.Pending;
            Total = total;
        }
    }
}