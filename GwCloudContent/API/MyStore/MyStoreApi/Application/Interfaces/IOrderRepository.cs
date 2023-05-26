using MyStoreApi.Domain.Models;

namespace MyStoreApi.Application.Interfaces
{
    public interface IOrderRepository
    {
        Order GetOrderById(Guid id);
        void CreateOrder(Order newOrder);
        void RemoveProduct(OrderProduct orderProduct);
        void Save();
    }
}