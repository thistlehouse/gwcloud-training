namespace PaymentProcessor.Microservice.Broker
{
    public class PaymentLoad
    {
        public Guid OrderId { get; set; }
        public PaymentStatus Status { get; set; }

        public PaymentLoad() {}
    }
}