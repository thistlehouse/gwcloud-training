using MyStoreApi.Contracts.OrderDto;
using MyStoreApi.UseCases;

namespace MyStoreApi.Application.UseCases.OrderUseCases.GetOrderByIdUseCase
{
    public interface IGetOrderByIdUseCase
    {
        ServiceResponse<OrderResponse> GetOrderById(OrderByIdRequest request);
    }
}