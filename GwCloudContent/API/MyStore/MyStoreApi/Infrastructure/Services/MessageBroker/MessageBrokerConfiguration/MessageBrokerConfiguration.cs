namespace MyStoreApi.Infrastructure.Services.MessageBroker.MessageBrokerConfiguration
{
    public class MessageBrokerConfiguration
    {
        public string HostName { get; set; }
        public int Port { get; set; }
        public string Queue { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string VirtualHost { get; set; }
    }
}