using System.Text.Json.Serialization;
using MyStoreApi.Domain.Validator;

namespace MyStoreApi.Domain.Models
{
    public class Product : Entity
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public List<OrderProduct> OrderProducts { get; private set; } = new();

        public Product() {}

        public Product(Guid id,
            string name,
            decimal price)
        {
            Id = id;
            Name = name;
            Price = price;

            Validate(this, new ProductValidator());
        }

        public Product(string name,
            decimal price,
            List<OrderProduct> orderProducts)
        {
            Name = name;
            Price = price;
            OrderProducts = orderProducts;

            Validate(this, new ProductValidator());
        }

        public Product(Guid id,
            string name,
            decimal price,
            List<OrderProduct> orderProducts)
        {
            Id = id;
            Name = name;
            Price = price;
            OrderProducts = orderProducts;

            Validate(this, new ProductValidator());
        }
    }
}