using Autofac;
using MyStoreApi.Application.Services;
using MyStoreApi.Infrastructure.Services.MessageBroker;

namespace MyStoreApi.Infrastructure.Services.AutofacServiceModule
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MessageProducer>()
                .As<IMessageProducer>()
                .InstancePerLifetimeScope();
        }
    }
}