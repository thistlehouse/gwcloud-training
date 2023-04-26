using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DI.Services
{
    public class InputReaderService : IInputReaderService
    {
        public Dictionary<string, decimal> GetNumbers(bool canUseZero)
        {
            decimal leftNumber, rightNumber;
            
            bool left, right = false;

            do
            {
                Console.WriteLine("Type the first number");
                left = decimal.TryParse(Console.ReadLine(), out leftNumber);

                if (!left)        
                    Console.WriteLine("Invalid input. Type a key to continue.");  

                Console.Clear();      
            } while (!left);

            do
            {
                Console.WriteLine("Type the second number");
                right = decimal.TryParse(Console.ReadLine(), out rightNumber);

                if (!right)
                {
                    Console.WriteLine("Invalid input. Type a key to continue.");
                }
                
                while (!canUseZero)
                {
                    Console.Clear();
                    Console.WriteLine("Number must be greater than zero.");
                    Console.WriteLine("Type the second number again");
                    right = decimal.TryParse(Console.ReadLine(), out rightNumber);

                    if (rightNumber > 0)
                        canUseZero = true;
                }

                Console.Clear();
            } while (!right);

            Dictionary<string, decimal> numbers = new Dictionary<string, decimal>()
            {
                {"leftNumber", leftNumber},
                {"rightNumber", rightNumber}
            };

            return  numbers;
        }

        public int MenuOptionIndex()
        {
            int readInput = 0;
            bool success = false;
            
            success = int.TryParse(Console.ReadLine(), out readInput);

            while (!success)
            {          
                Console.WriteLine("Invalid input. Type a key to continue.");
                Console.ReadKey();
            }

            Console.Clear();

            return readInput;
        }

        public void PressAnyKeyToContinue()
        {
            Console.WriteLine("Press any keyto continue.");
            Console.ReadKey(); 
            Console.Clear(); 
        }
    }
}