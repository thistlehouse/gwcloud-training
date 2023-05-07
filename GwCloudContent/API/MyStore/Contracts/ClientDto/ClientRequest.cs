using MyStore.Models;

namespace MyStore.Contracts.ClientDto
{
    public class ClientRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Order> Orders { get; set; } = new();
    }
}