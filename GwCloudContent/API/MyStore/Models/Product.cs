namespace MyStore.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public List<OrderProduct> OrderProducts { get; set; } = new();

        public Product() {}

        public Product(Guid id,
            string name,
            decimal price)
        {
            Id = id;
            Name = name;
            Price = price;            
        }

        public Product(string name,
            decimal price,
            List<OrderProduct> orderProducts)
        {
            Name = name;
            Price = price;
            OrderProducts = orderProducts;
        }
    }
}