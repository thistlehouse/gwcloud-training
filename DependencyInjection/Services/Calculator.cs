namespace DI.Domain;

using DI.Services;

public class Calculator : ICalculator
{
    public decimal leftNumber { get; set; }
    public decimal rightNumber { get; set; }      
    private bool exit = true;

    private readonly ICalculatorService _calculatorService;
    private readonly IDisplayMenuService _displayMenuService;
    
    public Calculator(ICalculatorService calculatorService,  IDisplayMenuService displayMenuService)
    {
        _calculatorService = calculatorService;
        _displayMenuService = displayMenuService;
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
                    Console.Clear();
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
        _displayMenuService.DisplayMainMenu(); 
    }
    
    Dictionary<string, decimal> GetNumbers(bool canUseZero)
    {
        return _displayMenuService.GetNumbers(canUseZero);                  
    }

    void PressAnyKeyToContinue()
    {
       _displayMenuService.PressAnyKeyToContinue();
    }
}