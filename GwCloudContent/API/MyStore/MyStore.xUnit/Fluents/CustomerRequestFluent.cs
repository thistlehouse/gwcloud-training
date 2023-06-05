using MyStoreApi.Contracts.CustomerDto;
using MyStoreApi.Domain.Models;

namespace MyStore.xUnit.Fluents
{
    public class CustomerRequestFluent
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public List<Order> Orders { get; private set; } = new();

        public static CustomerRequestFluent New()
        {
            return new CustomerRequestFluent()
            {
                Id = Guid.NewGuid(),
                Name = "Customer Test Name"
            };
        }

        public CustomerRequest Build() =>
            new CustomerRequest(Id, Name);

        public CustomerRequestFluent WithId(Guid id)
        {
            Id = id;
            return this;
        }

        public CustomerRequestFluent WithName(string name)
        {
            Name = name;
            return this;
        }
    }
}