using System.Linq;
using DI.Domain;

namespace DI.Services;

public class DisplayMenuService : IDisplayMenuService
{
    private readonly IMathematicOperation _mathematicOperation;
    
    public DisplayMenuService(IMathematicOperation mathematicOperation, IInputReaderService inputReaderService)
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
        //mathematicOperation.Create(new string[]{"Addition", "Subtraction"});
        var operation = CreateMenu();  
        
        Console.WriteLine("========== Calculator ==========");

        operation.ForEach(o => Console.WriteLine(o.MathOperation));
    }
}