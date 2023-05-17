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
        }

        public Customer(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Customer(Guid id, string name, List<Order> orders)
        {
            Id = id;
            Name = name;
            Orders = orders;
        }
    }
}