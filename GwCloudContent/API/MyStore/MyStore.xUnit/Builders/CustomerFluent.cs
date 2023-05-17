using MyStoreApi.Domain.Models;

namespace MyStore.xUnit.Builders
{
    public class CustomerFluent
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public static CustomerFluent New()
        {
            return new CustomerFluent()
            {
                Id = Guid.NewGuid(),
                Name = "Customer Name to Test",
            };
        }

        public Customer Build()
            => new Customer(Id, Name);

        public CustomerFluent WithId(Guid id)
        {
            Id = id;
            return this;
        }

        public CustomerFluent WithEmptyId(Guid id)
        {
            Id = id;
            return this;
        }

        public CustomerFluent WithName(string name)
        {
            Name = name;
            return this;
        }
    }
}