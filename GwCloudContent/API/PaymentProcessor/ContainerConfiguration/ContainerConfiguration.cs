using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PaymentProcessor.MessageConsumer;

namespace PaymentProcessor.ContainerConfiguration
{
    public static class ContainerConfiguration
    {
        public static IHost Configure()
        {
            var builder =  Host.CreateDefaultBuilder();

            builder.ConfigureServices(services =>
                services.AddHostedService<PaymentConsumer>()
            );

            return builder.Build();
        }
    }
}