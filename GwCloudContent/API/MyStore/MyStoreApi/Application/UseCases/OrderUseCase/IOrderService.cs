using MyStoreApi.Contracts.OrderDto;
using MyStoreApi.Contracts.OrderProductDto;
using MyStoreApi.Domain.Models;
using MyStoreApi.UseCases;

namespace MyStoreApi.Application.Interfaces
{
    public interface IOrderService
    {
        ServiceResponse<OrderResponse> GetOrderById(OrderByIdRequest request);
        ServiceResponse<OrderResponse> CreateOrder(OrderRequest request);
        ServiceResponse<OrderResponse> UpSertProduct(OrderProductRequest request);
        ServiceResponse<OrderResponse> RemoveProduct(OrderProductRequest request);
    }
}