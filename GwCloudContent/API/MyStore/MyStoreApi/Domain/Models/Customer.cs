using MyStoreApi.Domain.Validators;

namespace MyStoreApi.Domain.Models
{
    public class Customer : Entity
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public List<Order> Orders { get; private set; } = new();

        public Customer() {}

        public Customer(Guid id)
        {
            Id = id;

            Validate(this, new CustomerValidator());
        }

        public Customer(string name)
        {
            Name = name;

            Validate(this, new CustomerValidator());
        }

        public Customer(Guid id, string name)
        {
            Id = id;
            Name = name;

            Validate(this, new CustomerValidator());
        }

        public Customer(Guid id, string name, List<Order> orders)
        {
            Id = id;
            Name = name;
            Orders = orders;

            Validate(this, new CustomerValidator());
        }
    }
}