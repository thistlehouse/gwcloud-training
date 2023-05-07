using MyStore.Models;

namespace MyStore.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Order GetOrderById(Guid id);
        void CreateOrder(Order newOrder);
        Order UpdateOrder(Order order);        
        void RemoveProduct(OrderProduct orderProduct);
        void Save();
    }
}