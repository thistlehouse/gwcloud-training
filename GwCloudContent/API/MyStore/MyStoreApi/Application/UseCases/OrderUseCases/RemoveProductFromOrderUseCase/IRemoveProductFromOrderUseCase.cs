using MyStoreApi.Contracts.OrderDto;
using MyStoreApi.Contracts.OrderProductDto;
using MyStoreApi.UseCases;

namespace MyStoreApi.Application.UseCases.OrderUseCases.RemoveProductFromOrderUseCase
{
    public interface IRemoveProductFromOrderUseCase
    {
        ServiceResponse<OrderResponse> RemoveProduct(OrderProductRequest request);
    }
}