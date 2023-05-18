using Autofac;
using MyStoreApi.Application.UseCases.ProductUserCase;
using MyStoreApi.Contracts.CustomerDto;
using MyStoreApi.Contracts.OrderDto;
using MyStoreApi.Contracts.ProductDto;
using MyStoreApi.Application.UseCases.CustomerUseCase;
using MyStoreApi.UseCases;
using MyStoreApi.UseCases.ProductUserCase;
using MyStoreApi.Application.UseCases.OrderUseCase;
using MyStoreApi.Application.Interfaces;

namespace MyStoreApi.Application.UseCases.AutofacModule
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CustomerService>().As<ICustomerService>();
            builder.RegisterType<OrderService>().As<IOrderService>();
            builder.RegisterType<ProductService>().As<IProductService>();
            builder.RegisterType<ServiceResponse<ProductResponse>>().AsSelf();
            builder.RegisterType<ServiceResponse<OrderResponse>>().AsSelf();
            builder.RegisterType<ServiceResponse<CustomerResponse>>().AsSelf();

        }
    }
}