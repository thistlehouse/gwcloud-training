using System.Text.Json.Serialization;
using MyStoreApi.Domain.Models;

namespace MyStoreApi.Contracts.OrderDto
{
    public class NewOrderRequest
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }

        [JsonIgnore]
        public Customer Customer { get; set; } 
        public List<OrderProduct> OrderProducts { get; set; } = new();
        public decimal TotalToPay { get; set; }       
    }
}