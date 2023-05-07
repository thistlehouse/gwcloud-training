using MyStore.Models;

namespace MyStore.Contracts.OrderProductDto
{
    public class OrderProductRequest
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
    }
}