using System.Text.Json.Serialization;
using MyStoreApi.Domain.Validators;

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

            Validate(this, new OrderProductValidator());
        }

        public OrderProduct(Guid productId, int quantity, decimal total)
        {
            ProductId = productId;
            Quantity = quantity;
            Total = total;

            Validate(this, new OrderProductValidator());
        }
    }
}