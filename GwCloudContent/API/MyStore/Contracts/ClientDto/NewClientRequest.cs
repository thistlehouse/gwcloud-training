using MyStore.Models;

namespace MyStore.Contracts.ClientDto
{
    public class NewClientRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Order> Orders { get; set; } = new();
    }
}