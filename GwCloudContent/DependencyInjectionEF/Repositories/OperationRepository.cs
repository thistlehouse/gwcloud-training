using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DI.Domain;
using DI.Persistence;

namespace DIEF.Repositories
{
    public class OperationRepository : IOperationRepository
    {
        private readonly DIDbContext _diDbContext;

        public OperationRepository(DIDbContext diDbContext)
        {
            _diDbContext = diDbContext;
        }

        public List<Operation> GetAll()
        {
            return _diDbContext.Operations.ToList();
        }

        public Guid Create(Operation operation)
        {
            _diDbContext.Operations.Add(operation);
            _diDbContext.SaveChanges();

            return operation.Id;
        }

        public void Save()
        {
        }

        public List<Operation> LastTenOperations()
        {
            return _diDbContext.Operations
                .OrderByDescending(o => o.Id)
                .Take(10)
                .ToList();
        }
    }
}