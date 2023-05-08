namespace MyStore.Models
{
    public class Customer
    {   
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Order> Orders { get; set; } = new();

        public Customer() {} 

        public Customer(Guid id)
        {
            Id = id;
        }

        public Customer(Guid id, string name, List<Order> orders)
        {
            Id = id;
            Name = name;
            Orders = orders;
        } 
    }
}