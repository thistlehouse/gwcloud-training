namespace PaymentProcessor.Microservice.Broker
{
    public class BrokerSettings
    {
        public string HostName { get; set; } = "localhost";
        public int Port{ get; set; } = 5672;
    }
}