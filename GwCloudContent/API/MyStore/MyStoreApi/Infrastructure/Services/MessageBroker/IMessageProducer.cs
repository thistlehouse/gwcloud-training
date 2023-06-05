namespace MyStoreApi.Infrastructure.Services.MessageBroker
{
    public interface IMessageProducer
    {
        void Publisher<T>(T message, string queue);
        T Consumer<T>(string queue);
    }
}