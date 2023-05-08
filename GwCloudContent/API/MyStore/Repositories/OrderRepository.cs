using Microsoft.EntityFrameworkCore;
using MyStore.Models;
using MyStore.Persistence;
using MyStore.Repositories.Interfaces;

namespace MyStore.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly MyStoreDbContext _myStoreContext;

        public OrderRepository(MyStoreDbContext myStoreContext)
        {
            _myStoreContext = myStoreContext;
        }

        public void CreateOrder(Order newOrder)
        {
            _myStoreContext.Orders.Add(newOrder);
            _myStoreContext.SaveChanges();
        }

        public Order UpdateOrder(Order order)
        {
            _myStoreContext.Orders.Update(order);
            _myStoreContext.SaveChanges();

            return order;
        }

        public Order GetOrderById(Guid id)
        {
            return _myStoreContext.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderProducts)                                
                .FirstOrDefault(o => o.Id == id);
        }

        public void RemoveProduct(OrderProduct orderProduct)
        {
            // _myStoreContext.OrdersProducts
            //     .Find(orderProduct.OrderId)
            //     .Remove(orderProduct);

            Save();
        }

        public void Save()
        {
            _myStoreContext.SaveChanges();
        }
    }
}