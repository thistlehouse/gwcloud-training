using MyStore.Persistence;
using MyStore.Repositories;
using MyStore.Repositories.Interfaces;
using MyStore.Services;
using MyStore.Services.Interfaces;

namespace MyStore
{
    public static class ContainerConfiguration
    {
        public static WebApplication Configure()
        {
            var builder = WebApplication.CreateBuilder();

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddDbContext<MyStoreDbContext>();            
            builder.Services.AddEndpointsApiExplorer();   
            builder.Services.AddScoped<IClientRepository, ClientRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<IClientService, ClientService>();            
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<IProductService, ProductService>();            
            return builder.Build();
        }
    }
}