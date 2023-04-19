using System.Linq;
using DI.Domain;

namespace DI.Services;

public class DisplayMenuService : IDisplayMenuService
{
    private readonly IMathematicOperation _mathematicOperation;

    public DisplayMenuService(IMathematicOperation mathematicOperation)
    {
        _mathematicOperation = mathematicOperation;
    }

    public List<Operation> CreateMenu()
    {
        var operations = new List<Operation>();

        operations.Add(_mathematicOperation.Create("1 - Addition"));
        operations.Add(_mathematicOperation.Create("2 - Subtraction"));
        operations.Add(_mathematicOperation.Create("3 - Multiplication"));
        operations.Add(_mathematicOperation.Create("4 - Division"));
        operations.Add(_mathematicOperation.Create("0 - Exit"));

        return operations;
    }

    public void DisplayMainMenu()
    {
        //mathematicOperation.Create(new string[]{"Soma", "Subtração"});
        var operation = CreateMenu();  
        
        Console.WriteLine("========== Calculator ==========");

        operation.ForEach(o => Console.WriteLine(o.MathOperation));
    }

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

    public void PressAnyKeyToContinue()
    {
        Console.WriteLine("Press any keyto continue.");
        Console.ReadKey(); 
        Console.Clear(); 
    }
}