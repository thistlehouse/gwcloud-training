using System.Text.Json.Serialization;

namespace MyStore.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }

        [JsonIgnore]
        public Client Client { get; set; } 
        public List<OrderProduct> OrderProducts { get; set; } = new();
        public decimal TotalToPay { get; set; }

        public Order() {}

        public Order(Guid id,
            Guid clientId, 
            List<OrderProduct> orderProducts,
            decimal totalToPay) 
        {
            Id = id;
            ClientId = clientId;
            OrderProducts = orderProducts;
            TotalToPay = totalToPay;
        }

        public Order( 
            Guid clientId, 
            List<OrderProduct> orderProducts,
            decimal totalToPay) 
        {
            ClientId = clientId;
            OrderProducts = orderProducts;
            TotalToPay = totalToPay;
        }
    }
}