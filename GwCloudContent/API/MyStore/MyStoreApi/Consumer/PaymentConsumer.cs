using System.Text;
using MyStoreApi.Contracts.PaymentDto;
using MyStoreApi.Domain.Models;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MyStoreApi.Consumer
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

                    Console.WriteLine($"\nMessage received From \"PaymentConsumer\": {payment}");

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

                        Console.WriteLine($"Message sent from PaymentProcess #response: {paymentResponse}");
                    }

                     _channel.BasicAck(ea.DeliveryTag, false);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
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