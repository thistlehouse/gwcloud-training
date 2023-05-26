using MyStoreApi.Infrastructure.Services.MessageBroker.MessageBrokerConfiguration;
using System.Text;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using MyStoreApi.Infrastructure.Services.MessageBroker;

namespace MyStoreApi.Application.Services
{
    public class MessageProducer : IMessageProducer
    {
        private readonly MessageBrokerConfiguration _messageBrokerConfiguration;
        private readonly ConnectionFactory _connectionFactory;

        public MessageProducer(IOptions<MessageBrokerConfiguration> options)
        {
            _messageBrokerConfiguration = options.Value;

            _connectionFactory = new ConnectionFactory
            {
                HostName = _messageBrokerConfiguration.Host,
                Port = _messageBrokerConfiguration.Port,
                UserName = _messageBrokerConfiguration.UserName,
                Password = _messageBrokerConfiguration.Password,
                VirtualHost = _messageBrokerConfiguration.VirtualHost
            };
        }

        public void Publisher<T>(T message, string queue)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(
                        queue: queue,
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null
                    );

                    var requestMessage = JsonConvert.SerializeObject(message);
                    var byteMessage = Encoding.UTF8.GetBytes(requestMessage);

                    channel.BasicPublish(
                        exchange: "",
                        routingKey: queue,
                        basicProperties: null,
                        body: byteMessage
                    );
                }
            }
        }

        public T Consumer<T>(string queue)
        {
            var connection = _connectionFactory.CreateConnection();
            var channel = connection.CreateModel();

            channel.QueueDeclare(
                queue: queue,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );

            var consumer = new EventingBasicConsumer(channel);

            string message = "";

            consumer.Received += (sender, eventArgs) =>
            {
                var contentArray = eventArgs.Body.ToArray();
                message = Encoding.UTF8.GetString(contentArray);

                channel.BasicAck(eventArgs.DeliveryTag, false);
            };

            channel.BasicConsume(queue, false, consumer);

            return JsonConvert.DeserializeObject<T>(message);
        }
    }
}