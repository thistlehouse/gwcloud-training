using Autofac;
using AutoMapper;
using MyStoreApi.Contracts.CustomerDto;
using MyStoreApi.Contracts.OrderDto;
using MyStoreApi.Contracts.OrderProductDto;
using MyStoreApi.Contracts.ProductDto;
using MyStoreApi.Domain.Models;

namespace MyStoreApi.Contracts.AutofacModule
{
    public class AutoMapperModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CustomerRequest, Customer>(); // .ReverseMap()
                cfg.CreateMap<CustomerByIdRequest, Customer>(); // .ReverseMap()
                cfg.CreateMap<Customer, CustomerResponse>();

                cfg.CreateMap<ProductRequest, Product>(); // .ReverseMap()
                cfg.CreateMap<ProductByIdRequest, Product>(); // .ReverseMap()
                cfg.CreateMap<Product, ProductResponse>(); // .ReverseMap()

                cfg.CreateMap<OrderRequest, Order>();
                cfg.CreateMap<NewOrderRequest, Order>();
                cfg.CreateMap<OrderByIdRequest, Order>();
                cfg.CreateMap<Order, OrderResponse>();

                cfg.CreateMap<OrderProductRequest, OrderProduct>().ReverseMap();
            })).AsSelf().SingleInstance();

            builder.Register(c => c.Resolve<MapperConfiguration>()
                .CreateMapper(c.Resolve))
                .As<IMapper>()
                .InstancePerLifetimeScope();
        }
    }
}