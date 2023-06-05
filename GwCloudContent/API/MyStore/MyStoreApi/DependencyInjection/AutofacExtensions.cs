using Autofac;
using MyStoreApi.Application.UseCases.AutofacModule;
using MyStoreApi.Application.Interfaces.AutofacModule;
using MyStoreApi.Infrastructure.Persistence.AutofacModule;
using MyStoreApi.Contracts.AutofacModule;
using MyStoreApi.Infrastructure.Services.AutofacServiceModule;

namespace MyStoreApi.DependencyInjection
{
    public static class AutofacExtensions
    {
        public static ContainerBuilder AddAutofacRegistration(this ContainerBuilder builder)
        {
            builder.RegisterModule(new DbContextModule());
            builder.RegisterModule(new UseCaseModule());
            builder.RegisterModule(new ServiceModule());
            builder.RegisterModule(new RepositoryModule());
            builder.RegisterModule(new AutoMapperModule());

            return builder;
        }
    }
}
