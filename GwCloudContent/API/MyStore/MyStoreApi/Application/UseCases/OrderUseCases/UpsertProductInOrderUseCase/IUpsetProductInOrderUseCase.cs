using MyStoreApi.Contracts.OrderDto;
using MyStoreApi.Contracts.OrderProductDto;
using MyStoreApi.UseCases;

namespace MyStoreApi.Application.UseCases.OrderUseCases.UpsertProductInOrderUseCase
{
    public interface IUpsertProductInOrderUseCase
    {
        ServiceResponse<OrderResponse> UpSertProduct(OrderProductRequest request);
    }
}