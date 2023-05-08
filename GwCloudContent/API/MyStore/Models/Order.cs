using System.Text.Json.Serialization;

namespace MyStore.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }

        [JsonIgnore]
        public Customer Customer { get; set; } 
        public List<OrderProduct> OrderProducts { get; set; } = new();
        public decimal TotalToPay { get; set; }

        public Order() {}

        public Order(Guid id,
            Guid CustomerId, 
            List<OrderProduct> orderProducts,
            decimal totalToPay) 
        {
            Id = id;
            CustomerId = CustomerId;
            OrderProducts = orderProducts;
            TotalToPay = totalToPay;
        }

        public Order( 
            Guid CustomerId, 
            List<OrderProduct> orderProducts,
            decimal totalToPay) 
        {
            CustomerId = CustomerId;
            OrderProducts = orderProducts;
            TotalToPay = totalToPay;
        }
    }
}