using MyStoreApi.Domain.Validator;

namespace MyStoreApi.Domain.Models
{
    public class Customer : Entity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Order> Orders { get; set; } = new();

        public Customer() {}

        public Customer(Guid id)
        {
            Id = id;

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