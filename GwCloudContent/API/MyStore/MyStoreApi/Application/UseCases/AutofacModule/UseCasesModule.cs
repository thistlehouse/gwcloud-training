using Autofac;
using MyStoreApi.Contracts.CustomerDto;
using MyStoreApi.Contracts.OrderDto;
using MyStoreApi.Contracts.ProductDto;
using MyStoreApi.UseCases;
using MyStoreApi.Application.Interfaces;
using MyStoreApi.Application.UseCases.PaymentUseCases.ProcessOrderPayment;
using MyStoreApi.Infrastructure.Services;
using MyStoreApi.Application.UseCases.ProductUseCases.CreateProductUseCase;
using MyStoreApi.Application.UseCases.ProductUseCases.GetProductByIdUseCase;
using MyStoreApi.Application.UseCases.ProductUseCases.UpdateProductUseCase;
using MyStoreApi.Application.UseCases.OrderUseCases.CreateOrderUseCase;
using MyStoreApi.Application.UseCases.OrderUseCases.GetOrderByIdUseCase;
using MyStoreApi.Application.UseCases.OrderUseCases.UpsertProductInOrderUseCase;
using MyStoreApi.Application.UseCases.OrderUseCases.UpdateOrderStatusUseCase;
using MyStoreApi.Application.UseCases.OrderUseCases.RemoveProductFromOrderUseCase;

namespace MyStoreApi.Application.UseCases.AutofacModule
{
    public class UseCaseModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // builder.RegisterType<OrderService>().As<IOrderService>();

            builder.RegisterType<CreateOrderUseCase>().As<ICreateOrderUseCase>();
            builder.RegisterType<GetOrderByIdUseCase>().As<IGetOrderByIdUseCase>();
            builder.RegisterType<UpsertProductInOrderUseCase>().As<IUpsertProductInOrderUseCase>();
            builder.RegisterType<UpdateOrderStatusUseCase>().As<IUpdateOrderStatusUseCase>();
            builder.RegisterType<RemoveProductFromOrderUseCase>().As<IRemoveProductFromOrderUseCase>();

            builder.RegisterType<CreateProductUseCase>().As<ICreateProductUseCase>();
            builder.RegisterType<GetProductByIdUseCase>().As<IGetProductByIdUseCase>();
            builder.RegisterType<UpdateProductUseCase>().As<IUpdateProductUseCase>();

            builder.RegisterType<PaymentService>().As<IPaymentService>();

            builder.RegisterType<PayOrderUseCase>().As<IPayOrderUseCase>();

            builder.RegisterType<ServiceResponse<ProductResponse>>().AsSelf();
            builder.RegisterType<ServiceResponse<OrderResponse>>().AsSelf();
            builder.RegisterType<ServiceResponse<CustomerResponse>>().AsSelf();
        }
    }
}