using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DI.Services
{
    public interface IInputReaderService
    {
        int MenuOptionIndex();
        Dictionary<string, decimal> GetNumbers(bool canUseZero);
        void PressAnyKeyToContinue();
    }
}