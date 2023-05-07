using MyStore.Contracts.ClientDto;
using MyStore.Contracts.OrderProductDto;
using MyStore.Models;

namespace MyStore.Contracts.OrderDto
{
    public class OrderRequest
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public List<OrderProduct> OrderProducts { get; set; } = new();
        public decimal TotalToPay { get; set; }
    }
}