using MyStore.Contracts.OrderProductDto;

namespace MyStore.Contracts.ProductDto
{
    public class ProductRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public List<OrderProductRequest> OrderProducts { get; set; } = new();
    }
}