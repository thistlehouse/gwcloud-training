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

        public Order GetOrderById(Guid id)
        {
            return _myStoreApiContext.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderProducts)
                .Where(o => o.Id == id)
                .FirstOrDefault();
        }

        public void RemoveProduct(OrderProduct orderProduct)
        {
            _myStoreApiContext.OrdersProducts.Remove(orderProduct);

            Save();
        }

        public void Save()
        {
            _myStoreApiContext.SaveChanges();
        }
    }
}