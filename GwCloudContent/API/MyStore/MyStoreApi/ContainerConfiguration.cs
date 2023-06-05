using Autofac;
using Autofac.Extensions.DependencyInjection;
using MyStoreApi.Consumer;
using MyStoreApi.DependencyInjection;
using MyStoreApi.Infrastructure.Services.MessageBroker.MessageBrokerConfiguration;

namespace MyStoreApi
{
    public static class ContainerConfiguration
    {
        public static WebApplication Configure()
        {
            var builder = WebApplication.CreateBuilder();

            // Autofac Dependency Injection
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            builder.Host.ConfigureContainer<ContainerBuilder>((hbc, builder) =>
                builder.AddAutofacRegistration());

            // Microsoft Dependency Injection
            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.Configure<MessageBrokerConfiguration>(builder.Configuration.GetSection("RabbitMqConfig"));
            // builder.Services.AddHostedService<PaymentConsumer>();
            builder.Services.AddHostedService<OrderConsumer>();
            // builder.Services.AddDbContext<MyStoreApiDbContext>();
            // builder.Services.AddAutoMapper(typeof(Program).Assembly);
            // builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
            // builder.Services.AddScoped<IProductRepository, ProductRepository>();
            // builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            // builder.Services.AddScoped<ICustomerService, CustomerService>();
            // builder.Services.AddScoped<IOrderService, OrderService>();
            // builder.Services.AddScoped<IProductService, ProductService>();

            return builder.Build();
        }
    }
}

