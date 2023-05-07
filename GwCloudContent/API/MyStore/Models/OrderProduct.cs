namespace MyStore.Models
{
    public class OrderProduct
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public Guid OrderId { get; set; }
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