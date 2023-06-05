using System.Text;
using Microsoft.Extensions.Options;
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
        private readonly MessageBrokerConfiguration _messageBrokerConfiguration;
        private IModel _channel;
        private IConnection _connection;
        private ConnectionFactory _connectionFactory;

        private readonly IUpdateOrderStatusUseCase _updateOrderStatus;

        public OrderConsumer(IUpdateOrderStatusUseCase updateOrderStatus,
            IOptions<MessageBrokerConfiguration> options)
        {
            _updateOrderStatus = updateOrderStatus;
            _messageBrokerConfiguration = options.Value;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _connectionFactory = new ConnectionFactory()
            {
                HostName = _messageBrokerConfiguration.HostName,
                UserName = _messageBrokerConfiguration.UserName,
                Password = _messageBrokerConfiguration.Password,
                Port = _messageBrokerConfiguration.Port,
                VirtualHost = _messageBrokerConfiguration.VirtualHost
                // DispatchConsumersAsync = true; // in the ConnectionFactory configuration.
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

                    Console.WriteLine($"\n\nMessage received from PaymentProcessor \n\t# Status: {paymentLoadResult.Status}");

                    _updateOrderStatus.UpdateOrderStatus(paymentLoadResult);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
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