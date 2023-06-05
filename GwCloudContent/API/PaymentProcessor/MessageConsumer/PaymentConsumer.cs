using PaymentProcessor.Contracts;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Microsoft.Extensions.Hosting;
using System.Text;

namespace PaymentProcessor.MessageConsumer
{
    public class PaymentConsumer : BackgroundService
    {
        private IModel _channel;
        private IConnection _connection;
        private ConnectionFactory _connectionFactory;

        public PaymentConsumer()
        {

        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("\n# Payment Processing has started");

            _connectionFactory = new ConnectionFactory()
            {
                UserName = "guest",
                Password = "guest",
                HostName = "localhost",
                Port = 5672,
                VirtualHost = "/",
                // DispatchConsumersAsync = true
                // DispatchConsumersAsync = true in the ConnectionFactory configuration.
                // This is important to be able to use an async consumer.
                // If you don’t add this configuration no messages will be picked up by
                // the AsyncEventingBasicConsumer.
            };

            _connection = _connectionFactory.CreateConnection();
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