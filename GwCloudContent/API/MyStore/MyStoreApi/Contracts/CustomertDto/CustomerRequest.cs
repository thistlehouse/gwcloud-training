using MyStoreApi.Domain.Models;

namespace MyStoreApi.Contracts.CustomerDto
{
    public class CustomerRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Order> Orders { get; set; } = new();
    }
}