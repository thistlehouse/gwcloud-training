namespace MyStore.Models
{
    public class Client
    {   
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Order> Orders { get; set; } = new();

        public Client() {} 

        public Client(Guid id)
        {
            Id = id;
        }

        public Client(Guid id, string name, List<Order> orders)
        {
            Id = id;
            Name = name;
            Orders = orders;
        } 
    }
}