using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DI.Domain;

namespace DIEF.Models
{
    public class OperationResult
    {
        public Guid Id { get; set; }
        public decimal Result { get; set; } 
        public Guid OperationId { get; set; }        

        public OperationResult() {}
    }
}