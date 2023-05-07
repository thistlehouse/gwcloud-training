using System.Text.Json.Serialization;
using MyStore.Models;

namespace MyStore.Contracts.OrderDto
{
    public class NewOrderRequest
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }

        [JsonIgnore]
        public Client Client { get; set; } 
        public List<OrderProduct> OrderProducts { get; set; } = new();
        public decimal TotalToPay { get; set; }       
    }
}