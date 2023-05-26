using MyStoreApi.Contracts.OrderDto;
using MyStoreApi.Contracts.PaymentDto;
using MyStoreApi.UseCases;

namespace MyStoreApi.Application.UseCases.OrderUseCases.UpdateOrderStatusUseCase
{
    public interface IUpdateOrderStatusUseCase
    {
        ServiceResponse<OrderResponse> UpdateOrderStatus(PaymentLoad load);
    }
}