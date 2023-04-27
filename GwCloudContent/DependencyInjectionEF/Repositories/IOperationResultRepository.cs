using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DIEF.Models;

namespace DIEF.Repositories
{
    public interface IOperationResultRepository
    {
        List<OperationResult> GetAll();
        void Create(OperationResult operationResult);        
    }
}