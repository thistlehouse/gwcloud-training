using Autofac;
using MyStore.xUnit.Infrastructure.AutofacModule;
using MyStoreApi.Application.Interfaces.AutofacModule;
using MyStoreApi.Application.UseCases.AutofacModule;
using MyStoreApi.Contracts.AutofacModule;
using MyStoreApi.Infrastructure.Persistence.AutofacModule;
using Xunit;
using Xunit.Abstractions;
using Xunit.Frameworks.Autofac;

[assembly: TestFramework("MyStore.xUnit.ConfigureTestFramework", "MyStore.xUnit")]

namespace MyStore.xUnit
{
    public class ConfigureTestFramework : AutofacTestFramework
    {
        public ConfigureTestFramework(IMessageSink diagnosticMessageSink)
            : base(diagnosticMessageSink)
        {
        }

        protected override void ConfigureContainer(ContainerBuilder builder)
        {
            // configure your container
            // e.g. builder.RegisterModule<TestOverrideModule>();

            builder.RegisterModule(new DbContextTestModule());
            // builder.RegisterModule(new DbContextModule());
            builder.RegisterModule(new ServiceModule());
            builder.RegisterModule(new RepositoryModule());            
            builder.RegisterModule(new AutoMapperModule());
        }
    }
}