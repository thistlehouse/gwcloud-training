using Autofac;

namespace MyStoreApi.Infrastructure.Persistence.AutofacModule
{
    public class DbContextModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MyStoreApiDbContext>().AsSelf(); //.InstancePerLifetimeScope();
        }
    }
}