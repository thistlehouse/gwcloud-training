using Microsoft.EntityFrameworkCore;
using MyStoreApi.Domain.Models;
using MyStoreApi.Application.Interfaces;
using MyStoreApi.Infrastructure.Persistence;

namespace MyStoreApi.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly MyStoreApiDbContext _myStoreApiContext;

        public OrderRepository(MyStoreApiDbContext myStoreApiContext)
        {
            _myStoreApiContext = myStoreApiContext;
        }

        public void CreateOrder(Order newOrder)
        {
            _myStoreApiContext.Orders.Add(newOrder);

            Save();
        }

        public void UpdateOrder(Order order)
        {
            _myStoreApiContext.Orders.Update(order);

            Save();
        }

        public Order GetOrderById(Guid id)
        {
            return _myStoreApiContext.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderProducts)
                .First(o => o.Id == id);
        }

        public void RemoveProduct(OrderProduct orderProduct)
        {
            // _myStoreApiContext.OrdersProducts
            //     .Find(orderProduct.OrderId)
            //     .Remove(orderProduct);

            Save();
        }

        public void Save()
        {
            _myStoreApiContext.SaveChanges();
        }
    }
}