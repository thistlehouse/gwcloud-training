using Microsoft.EntityFrameworkCore;
using MyStore.Models;
using MyStore.Persistence;
using MyStore.Repositories.Interfaces;

namespace MyStore.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly MyStoreDbContext _myStoreContext;

        public ClientRepository(MyStoreDbContext myStoreContext)
        {
            _myStoreContext = myStoreContext;
        }

        public Client CreateClient(Client client)
        {
            _myStoreContext.Add(client);
            
            Save();

            return client;
        }
    
        public Client GetClientById(Guid id)
        {
            return _myStoreContext.Clients
                .Include(c => c.Orders)
                .ThenInclude(o => o.OrderProducts)
                .FirstOrDefault(c => c.Id == id);
        }

        public List<Client> GetClients()
        {
            return _myStoreContext.Clients.ToList();
        }

        public Client UpdateClient(Client client)
        {
            _myStoreContext.Clients.Update(client);

            Save();

            return client;

        }

        public void Save()
        {
            _myStoreContext.SaveChanges();
        }
    }
}