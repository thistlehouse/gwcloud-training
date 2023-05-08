using MyStore.Models;

namespace MyStore.Contracts.CustomerDto
{
    public class NewCustomerRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Order> Orders { get; set; } = new();
    }
}