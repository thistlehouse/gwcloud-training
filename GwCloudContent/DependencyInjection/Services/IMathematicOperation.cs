using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DI.Domain;

namespace DI.Services
{
    public interface IMathematicOperation
    {
        Operation Create(string operation);
        //List<Operation> Create(string[] operations);
    }
}