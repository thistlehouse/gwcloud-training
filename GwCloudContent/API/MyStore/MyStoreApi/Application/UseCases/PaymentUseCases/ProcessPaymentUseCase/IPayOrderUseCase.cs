using MyStoreApi.Contracts.PaymentDto;
using MyStoreApi.Domain.Models;
using MyStoreApi.UseCases;

namespace MyStoreApi.Application.UseCases.PaymentUseCases.ProcessOrderPayment
{
    public interface IPayOrderUseCase
    {
        ServiceResponse<PaymentResponse> PayOrder(PaymentRequest payment);
    }
}