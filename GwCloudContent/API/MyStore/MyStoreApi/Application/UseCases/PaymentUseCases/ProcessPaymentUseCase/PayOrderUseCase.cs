using System.Net;
using MyStoreApi.Application.Interfaces;
using MyStoreApi.Contracts.PaymentDto;
using MyStoreApi.Domain.Models;
using MyStoreApi.UseCases;

namespace MyStoreApi.Application.UseCases.PaymentUseCases.ProcessOrderPayment
{
    public class PayOrderUseCase : IPayOrderUseCase
    {
        private readonly IPaymentService _paymentService;
        private readonly IOrderRepository _orderRepository;

        public PayOrderUseCase(IOrderRepository orderRepository,
            IPaymentService paymentService)
        {
            _orderRepository = orderRepository;
            _paymentService = paymentService;
        }

        public ServiceResponse<PaymentResponse> PayOrder(PaymentRequest request)
        {
            var response = new ServiceResponse<PaymentResponse>();
            var order = _orderRepository.GetOrderById(request.OrderId);

            if (order is null)
                return response.Response(HttpStatusCode.NotFound,
                    "Order not found.",
                    false);

            Payment payment = new Payment(
                request.CreditCard,
                request.OrderId,
                order.OrderProducts.Sum(op => op.Total));

            if (payment.IsValid)
                _paymentService.ProcessPayment(payment);
            else
                return response.Response(HttpStatusCode.BadRequest,
                    "Payment invalid.",
                    false);

            return response.Response(HttpStatusCode.OK,
                "Payment sent",
                true);
        }
    }
}