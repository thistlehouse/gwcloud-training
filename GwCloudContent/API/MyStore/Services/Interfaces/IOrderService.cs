using MyStore.Contracts.OrderDto;
using MyStore.Contracts.OrderProductDto;
using MyStore.Models;

namespace MyStore.Services
{
    public interface IOrderService
    {
        Order GetOrderById(Guid id);
        Order CreateOrder(Order request);
        Order UpSertProduct(OrderProduct request);
        Order RemoveProduct(OrderProduct request);   
    }
}