using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DI.Domain;

namespace DI.Services
{
    public class MathematicOperation : IMathematicOperation
    {
        public Operation Create(string operacao)
        {
            return new Operation(operacao);
        }
    }
}