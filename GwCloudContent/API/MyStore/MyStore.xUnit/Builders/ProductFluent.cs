using MyStoreApi.Domain.Models;

namespace MyStore.xUnit.Builders
{
    public class ProductFluent
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }         
        
        public static ProductFluent New()
        {
            return new ProductFluent() 
            { 
                Id = Guid.NewGuid(),
                Name = "This is a Product to Test",
                Price = 10.00m
            };
        }

        public Product Build() => 
            new Product(Id, Name, Price);   

        public ProductFluent WithId(Guid id)
        {
            Id = id;
            return this;
        }

        public ProductFluent WithName(string name)
        {
            Name = name;
            return this;
        }

        public ProductFluent WithPrice(decimal price)
        {
            Price = price;
            return this;
        }
    }
}