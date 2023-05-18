using Autofac;
using MyStoreApi.Infrastructure.Persistence;

namespace MyStore.xUnit.Infrastructure.AutofacModule
{
    public class DbContextTestModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MyStoreDbContextMock>()
                .As<MyStoreApiDbContext>()
                .InstancePerLifetimeScope();
        }
    }
}