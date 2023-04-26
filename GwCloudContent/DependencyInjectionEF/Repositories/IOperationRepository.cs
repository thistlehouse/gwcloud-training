using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DI.Domain;

namespace DIEF.Repositories
{
    public interface IOperationRepository
    {
        List<Operation> GetAll();
        List<Operation> LastTenOperations();
        void Create(Operation operation);
        void Save();
    }
}