using System.Text.Json.Serialization;
using MyStoreApi.Domain.Validator;

namespace MyStoreApi.Domain.Models
{
    public class OrderProduct : Entity
    {
        public Guid ProductId { get; set; }
        [JsonIgnore]
        public Product Product { get; set; }
        public Guid OrderId { get; set; }
        [JsonIgnore]
        public Order Order { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }

        public OrderProduct() {}

        public OrderProduct(Guid orderId,
            Guid productId,
            int quantity,
            decimal total)
        {
            ProductId = productId;
            OrderId = orderId;
            Quantity = quantity;
            Total = total;
        }

        public OrderProduct(Guid productId, Guid orderId)
        {
            ProductId = productId;
            OrderId = orderId;
        }
    }
}