using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DI.Domain
{
    public class Operation
    {
        public string MathOperation { get; private set; }

        public Operation(string mathOperation)
        {
            MathOperation = mathOperation;
        }
    }
}
