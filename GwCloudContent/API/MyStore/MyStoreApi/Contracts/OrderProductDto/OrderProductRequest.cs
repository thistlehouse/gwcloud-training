using MyStoreApi.Domain.Models;

namespace MyStoreApi.Contracts.OrderProductDto
{
    public class OrderProductRequest
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}