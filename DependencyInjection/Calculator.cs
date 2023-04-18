namespace DI;

using DI.Services;

public class Calculator
{
    public decimal leftNumber { get; set; }
    public decimal rightNumber { get; set; }      
    private bool exit = true;

    private readonly ICalculatorService _calculatorService;
    
    public Calculator() {}
    
    public Calculator(ICalculatorService calculatorService)
    {
        _calculatorService = calculatorService; 
    }

    public void Run()
    {
        Console.Clear();

        while (exit)
        {
            int readInput = 0;

            bool success = false;

            while (!success && readInput == 0)
            {
                DisplayMainMenu();
                
                success = int.TryParse(Console.ReadLine(), out readInput);;

                if (!success)             
                {
                    Console.WriteLine("Invalid input. Type a key to continue.");
                    Console.ReadKey();                
                }

                Console.Clear();
            }

            switch (readInput)
            {
                case 0:
                    exit = false;
                    
                    Console.WriteLine("Exiting from application...");
                    Thread.Sleep(1000);
                    break;
                case 1:
                {
                    var numbers = GetNumbers(true);
                    var result =  _calculatorService.Add(numbers["leftNumber"], 
                                                        numbers["rightNumber"]);
                    
                    Console.WriteLine($"Result: {result}");
                    PressAnyKeyToContinue();
                    break;
                }
                case 2: 
                {
                    var numbers = GetNumbers(true);
                    var result =  _calculatorService.Subtract(numbers["leftNumber"], 
                                                            numbers["rightNumber"]);

                    Console.WriteLine($"Result: {result}");
                    PressAnyKeyToContinue();
                    break;
                }
                case 3: 
                {
                    var numbers = GetNumbers(true);
                    var result =  _calculatorService.Multiply(numbers["leftNumber"], 
                                                            numbers["rightNumber"]);

                    Console.WriteLine($"Result: {result}");
                    PressAnyKeyToContinue();
                    break;
                }
                case 4:
                {
                    var numbers = GetNumbers(false);
                    var result =  _calculatorService.Divide(numbers["leftNumber"], 
                                                            numbers["rightNumber"]);

                    Console.WriteLine($"Result: {result}");
                    PressAnyKeyToContinue(); 
                    break;
                }
            };
        }
    }

    public void DisplayMainMenu()
    {
        Console.WriteLine("========== Calculator ==========");
        Console.WriteLine("1 -  Add");
        Console.WriteLine("2 - Subtrac");
        Console.WriteLine("3 - Multiply");
        Console.WriteLine("4 - Divide");
        Console.WriteLine("0 - Exit");        
    }
    
    Dictionary<string, decimal> GetNumbers(bool canUseZero)
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

    void PressAnyKeyToContinue()
    {
        Console.WriteLine("Press any keyto continue.");
        Console.ReadKey(); 
        Console.Clear();        
    }
}