public class ProcessMessageConsumer : BackgroundService
    {
        private readonly RabbitMqConfiguration configuration;
        private readonly IConnection connection;
        private readonly IModel channel;

 

        public ProcessMessageConsumer(IOptions<RabbitMqConfiguration> option)
        {
            this.configuration = option.Value;
            var connectionFactory = new ConnectionFactory
            {
                HostName = configuration.Host,
                UserName = configuration.UserName,
                Password = configuration.Password,
                VirtualHost = configuration.VirtualHost
            };

 

            connection = connectionFactory.CreateConnection();
            channel = connection.CreateModel();
            channel.QueueDeclare(
                queue: configuration.Queue,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);
        }

 

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, eventArgs) =>
             {
                 var contentArray = eventArgs.Body.ToArray();
                 var contentString = Encoding.UTF8.GetString(contentArray);
                 var message = JsonConvert.DeserializeObject<MessageInputModel>(contentString);

 

                 channel.BasicAck(eventArgs.DeliveryTag, false);
             };

 

            channel.BasicConsume(configuration.Queue, false, consumer);

 

            return Task.CompletedTask;
        }

 

    }