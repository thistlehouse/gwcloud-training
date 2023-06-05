using System.Text;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace PaymentProcessor.Microservice.Broker
{
    public class PaymentConsumer : BackgroundService
    {
        private BrokerSettings _settings;
        private IModel _channel;
        private IConnection _connection;
        private ConnectionFactory _connectionFactory;

        public PaymentConsumer(IOptions<BrokerSettings> settings)
        {
            _settings = settings.Value;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("\n# Payment Processing has started");

            _connectionFactory = new ConnectionFactory()
            {
                HostName = _settings.HostName,
                Port = _settings.Port
            };

            _connection =  _connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(
                queue: "payment",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("# Payment Processing stopping...");

            _connection.Close();

            return base.StopAsync(cancellationToken);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (model, ea) =>
            {
                var contentArray = ea.Body.ToArray();

                try
                {
                    var contentString = Encoding.UTF8.GetString(contentArray);
                    var payment= JsonConvert.DeserializeObject<Payment>(contentString);

                    Console.WriteLine($"\nMessage received From \"MyStoreApi\" \n\t# Content: {contentString}");

                    // Start of treating payload

                    if (ValidatePayment(payment))
                    {
                        payment.Status = PaymentStatus.Paid;

                        var paymentLoad = new PaymentLoad()
                        {
                            OrderId = payment.OrderId,
                            Status = payment.Status
                        };

                        var paymentResponse = JsonConvert.SerializeObject(paymentLoad);
                        var byteMessage = Encoding.UTF8.GetBytes(paymentResponse);

                        // End of treating payload

                        _channel.BasicPublish(
                            exchange: "",
                            routingKey: "payment",
                            basicProperties: null,
                            body: byteMessage
                        );

                        Console.WriteLine($"\nMessage sent from \"MyStoreApi\" \n\t # Response: {paymentResponse}");
                    }

                    _channel.BasicAck(ea.DeliveryTag, true);
                }
                catch (Exception ex)
                {
                    _channel.BasicNack(ea.DeliveryTag, false, false);
                }
            };

            _channel.BasicConsume(
                queue: "payment",
                autoAck: false,
                consumer: consumer);

            return Task.CompletedTask;
        }

        private bool ValidatePayment(Payment paymentLoad)
        {
            return true;
        }
    }
}