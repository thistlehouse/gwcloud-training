namespace PaymentProcessor.Contracts
{
    public class PaymentLoad
    {
        public Guid OrderId { get; set; }
        public PaymentStatus Status { get; set; }

        public PaymentLoad() {}
    }
}