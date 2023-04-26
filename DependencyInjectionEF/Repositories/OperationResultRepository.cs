using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DI.Persistence;
using DIEF.Models;

namespace DIEF.Repositories
{
    public class OperationResultRepository : IOperationResultRepository
    {
        private readonly DIDbContext _diDbContext;

        public OperationResultRepository(DIDbContext diDbContext)
        {
            _diDbContext = diDbContext;
        }

        public void Create(OperationResult operationResult)
        {
            _diDbContext.OperationResults.Add(operationResult);
        }

        public List<OperationResult> GetAll()
        {
            return _diDbContext.OperationResults.ToList();
        }

        public void Save()
        {
            _diDbContext.SaveChanges();
        }

        public void Update(OperationResult OperationResult)
        {
            _diDbContext.OperationResults.Update(OperationResult);
        }
    }
}