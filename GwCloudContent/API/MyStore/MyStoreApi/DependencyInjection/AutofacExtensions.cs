using Autofac;
using Autofac.Extensions.DependencyInjection;
using MyStoreApi.Application.UseCases.AutofacModule;
using MyStoreApi.Application.Interfaces.AutofacModule;
using MyStoreApi.Infrastructure.Persistence.AutofacModule;
using MyStoreApi.Contracts.AutofacModule;
using System.Reflection;

namespace MyStoreApi.DependencyInjection
{
    public static class AutofacExtensions
    {
        public static ContainerBuilder AddAutofacRegistration(this ContainerBuilder builder)
        {            
            builder.RegisterModule(new DbContextModule());
            builder.RegisterModule(new ServiceModule());
            builder.RegisterModule(new RepositoryModule());            
            builder.RegisterModule(new AutoMapperModule());            
            
            return builder;
        }
    }
}
