using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DIEF.Models;

namespace DI.Domain
{
    public class Operation
    {
        public Guid Id { get; set; }
        public string MathOperation { get; set; }
        public decimal LeftNumber { get; set; }
        public decimal RightNumber { get; set; }
        public OperationResult OperationResult { get; set; }

        public Operation() {}

        public Operation(string mathOperation)
        {
            Id = new Guid();
            MathOperation = mathOperation;
        }
    }
}
