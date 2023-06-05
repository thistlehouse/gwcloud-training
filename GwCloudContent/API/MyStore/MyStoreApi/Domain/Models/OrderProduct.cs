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

        public OrderProduct(int quantity)
        {
            Quantity = quantity;

            Validate(this, new OrderProductValidator());
        }

        public OrderProduct(Guid productId, int quantity, decimal productPrice)
        {
            ProductId = productId;
            Quantity = quantity;
            Total = orderProductTotal(productPrice);

            Validate(this, new OrderProductValidator());
        }

        private decimal orderProductTotal(decimal productPrice)
        {
            return  productPrice * Quantity;
        }

        public void SetQuantity(int quantity)
        {
            Quantity = quantity;
        }
        public void SetTotal(decimal productPrice)
        {
            Total = productPrice * Quantity;
        }
    }

}