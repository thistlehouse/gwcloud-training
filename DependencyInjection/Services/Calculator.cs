namespace DI.Domain;

using System.Collections.Generic;
using DI.Services;

public class Calculator : ICalculator
{
    public decimal leftNumber { get; set; }
    public decimal rightNumber { get; set; }      
    private bool exit = true;

    private readonly IInputReaderService _inputReaderService;
    private readonly ICalculatorService _calculatorService;
    private readonly IDisplayMenuService _displayMenuService;

    public Calculator(ICalculatorService calculatorService, IDisplayMenuService displayMenuService, IInputReaderService inputReaderService)
    {
        _calculatorService = calculatorService;
        _displayMenuService = displayMenuService;
        _inputReaderService = inputReaderService;
    }

    public void Run()
    {
        Console.Clear();

        while (exit)
        {
            int readInput = -1;

            _displayMenuService.DisplayMainMenu();
                
            readInput = _inputReaderService.MenuOptionIndex();

            switch (readInput)
            {
                case 0:
                    exit = false;

                    Console.WriteLine("Exiting from application...");
                    Thread.Sleep(1000);
                    Console.Clear();
                    break;
                case 1:
                    PerformCalculation(readInput);
                    break;
                case 2:
                    PerformCalculation(readInput);
                    break;
                case 3:
                    PerformCalculation(readInput);
                    break;
                case 4:
                    PerformCalculation(readInput);
                    break;
                
            };
        }
    }

    public decimal PerformCalculation(int menuOptionIndex)
    {
        bool canUseZero = true;

        if (menuOptionIndex == 4)
        {
            canUseZero = false;
        }

        var numbers = _inputReaderService.GetNumbers(canUseZero);

        var result = menuOptionIndex switch
        {
            1 => _calculatorService.Add(numbers["leftNumber"], numbers["rightNumber"]),
            2 => _calculatorService.Subtract(numbers["leftNumber"], numbers["rightNumber"]),
            3 => _calculatorService.Multiply(numbers["leftNumber"], numbers["rightNumber"]),
            4 => _calculatorService.Divide(numbers["leftNumber"], numbers["rightNumber"]),
            _ => 0.0m
        };

        Console.WriteLine($"Result: {result}");

        Console.Clear();

        return result;
    }
}