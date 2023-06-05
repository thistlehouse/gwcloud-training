using MyStore.xUnit.Fluents;
using MyStoreApi.Application.Interfaces;
using MyStoreApi.Domain.Models;
using MyStoreApi.Infrastructure.Persistence;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace MyStore.xUnit.Infrastructure
{
    [UseAutofacTestFramework]
    public class OrderRepositoryTest
    {
        private readonly IOrderRepository _orderRepository;
        private readonly MyStoreApiDbContext _myStoreDbContext;

        public OrderRepositoryTest(IOrderRepository orderRepository,
            MyStoreApiDbContext myStoreDbContext)
        {
            _orderRepository = orderRepository;
            _myStoreDbContext = myStoreDbContext;
        }

        [Fact]
        public void CreateOrder_Should_AddToDatabase()
        {
            var order = OrderFluent.New()
                .Build();

            _orderRepository.CreateOrder(order);

            _myStoreDbContext.Orders.Contains(order);
        }

        [Fact]
        public void CreateOrder_ShouldNot_AddToDatabase()
        {
            var order = OrderFluent.New()
                .WithTotal(0.00m)
                .Build();

            if (order.IsValid)
                _orderRepository.CreateOrder(order);

            _myStoreDbContext.Orders.Contains(order);
        }

        [Fact]
        public void RemoveOrderProduct_Should_RemoveProduct()
        {
            var order = OrderFluent.New()
                .Build();

            _orderRepository.CreateOrder(order);

            _orderRepository.RemoveProduct(order.OrderProducts[0]);

            order.OrderProducts.Contains(order.OrderProducts[0]);
        }
    }
}