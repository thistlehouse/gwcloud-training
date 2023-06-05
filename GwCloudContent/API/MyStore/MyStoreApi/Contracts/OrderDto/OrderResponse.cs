using MyStoreApi.Domain.Models;
using MyStoreApi.Requests.OrderProduct;

namespace MyStoreApi.Contracts.OrderDto
{
    public record OrderResponse(
        Guid CustomerId,
        List<OrderProduct> OrderProducts,
        Coupon Coupon,
        string OrderStatus,
        decimal TotalToPay
    );

}