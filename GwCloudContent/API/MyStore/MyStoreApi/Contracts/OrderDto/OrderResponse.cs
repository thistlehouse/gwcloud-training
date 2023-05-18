using MyStoreApi.Domain.Models;

namespace MyStoreApi.Contracts.OrderDto
{
    public record OrderResponse(
        Guid CustomerId,
        List<OrderProduct> OrderProducts,
        Coupon Coupon,
        decimal TotalToPay
    );

}