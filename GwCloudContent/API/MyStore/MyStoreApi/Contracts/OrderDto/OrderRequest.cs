using MyStoreApi.Contracts.CustomerDto;
using MyStoreApi.Contracts.OrderProductDto;
using MyStoreApi.Domain.Models;

namespace MyStoreApi.Contracts.OrderDto
{
    public class OrderRequest
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Coupon Coupon { get; set; }
        public List<OrderProduct> OrderProducts { get; set; } = new();
    }
}