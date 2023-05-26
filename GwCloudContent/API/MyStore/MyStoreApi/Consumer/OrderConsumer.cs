using System.Text;
using MyStoreApi.Application.UseCases.OrderUseCases.UpdateOrderStatusUseCase;
using MyStoreApi.Contracts.PaymentDto;
using MyStoreApi.Domain.Models;
using MyStoreApi.Infrastructure.Services.MessageBroker.MessageBrokerConfiguration;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MyStoreApi.Consumer
{
    public class OrderConsumer : BackgroundService
    {
        private readonly MessageBrokerConfiguration _messageProducerConfiguration;
        private IModel _channel;
        private IConnection _connection;
        private ConnectionFactory _connectionFactory;

        private readonly IUpdateOrderStatusUseCase _updateOrderStatus;

        public OrderConsumer(IUpdateOrderStatusUseCase updateOrderStatus)
        {
            _updateOrderStatus = updateOrderStatus;
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
            // var status = _messageProducer.Consumer<int>("paymentStatus");
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (model, ea) =>
            {
                try
                {
                    var contentArray = ea.Body.ToArray();
                    var contentString = Encoding.UTF8.GetString(contentArray);
                    var paymentLoadResult = JsonConvert.DeserializeObject<PaymentLoad>(contentString);

                    Console.WriteLine($"Message received from OrderConsumer #Status: {paymentLoadResult.Status}");

                    _updateOrderStatus.UpdateOrderStatus(paymentLoadResult);

                }
                catch (Exception ex)
                {
                    throw;
                }
            };

            _channel.BasicConsume(
                queue: "payment",
                autoAck: true,
                consumer: consumer);

            return Task.CompletedTask;
        }
    }
}