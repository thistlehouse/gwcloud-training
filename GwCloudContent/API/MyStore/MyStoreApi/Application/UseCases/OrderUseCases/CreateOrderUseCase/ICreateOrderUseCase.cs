using MyStoreApi.Contracts.OrderDto;
using MyStoreApi.UseCases;

namespace MyStoreApi.Application.UseCases.OrderUseCases.CreateOrderUseCase
{
    public interface ICreateOrderUseCase
    {
        ServiceResponse<OrderResponse> CreateOrder(OrderRequest request);
    }
}