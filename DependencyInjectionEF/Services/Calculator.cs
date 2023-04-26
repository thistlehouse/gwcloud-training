using DI.Domain;
using DIEF.Models;
using DIEF.Repositories;
using Microsoft.Extensions.Hosting;

namespace DI.Services;

public class Calculator : BackgroundService, ICalculator
{
    public decimal leftNumber { get; set; }
    public decimal rightNumber { get; set; }      
    private bool exit = false;

    private readonly IInputReaderService _inputReaderService;
    private readonly ICalculatorService _calculatorService;
    private readonly IDisplayMenuService _displayMenuService;
    private readonly IMathematicOperation _mathematicOperation;
    private readonly IOperationResultRepository _operationResultRepository;
    private readonly IOperationRepository _operationRepository;

    public Calculator(ICalculatorService calculatorService,
        IDisplayMenuService displayMenuService,
        IInputReaderService inputReaderService,
        IOperationResultRepository operationResultRepository,
        IOperationRepository operationRepository,
        IMathematicOperation mathematicOperation)
    {
        _calculatorService = calculatorService;
        _displayMenuService = displayMenuService;
        _inputReaderService = inputReaderService;
        _operationResultRepository = operationResultRepository;
        _operationRepository = operationRepository;
        _mathematicOperation = mathematicOperation;
    }

    public void Run()
    {
        Console.Clear();

        while (!exit)
        {
            int readInput = -1;

            _displayMenuService.DisplayMainMenu();
                
            readInput = _inputReaderService.MenuOptionIndex();

            switch (readInput)
            {
                case 0:
                    exit = true;

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
                case 5: 
                    List<Operation> lastTenOperations = _operationRepository.LastTenOperations();
                
                    lastTenOperations.ForEach(o => Console.WriteLine(o.MathOperation));

                    _inputReaderService.PressAnyKeyToContinue();
                break;              
            };
        }
    }

    public decimal PerformCalculation(int menuOptionIndex)
    {        
        var numbers = _inputReaderService.GetNumbers();
        var result = 0.0m;

        try
        {
            result = menuOptionIndex switch
            {
                1 => _calculatorService.Add(numbers["leftNumber"], numbers["rightNumber"]),
                2 => _calculatorService.Subtract(numbers["leftNumber"], numbers["rightNumber"]),
                3 => _calculatorService.Multiply(numbers["leftNumber"], numbers["rightNumber"]),
                4 => _calculatorService.Divide(numbers["leftNumber"], numbers["rightNumber"]),            
                _ => 0.0m
            };            
        }
        catch (Exception e)
        {            
            Console.WriteLine($"Something went wrong: {e.Message}");  
        }


        OperationResult operationResult = new OperationResult();        

        operationResult.Id = new Guid();
        operationResult.Result = result;
        operationResult.LeftNumber = numbers["leftNumber"];
        operationResult.RightNumber = numbers["rightNumber"];

        switch (menuOptionIndex)
        {
            case 1:
                SaveCalculation("Addition", operationResult);
                break;
            case 2:
                SaveCalculation("Subtraction", operationResult);
                break;
            case 3:
                SaveCalculation("Multiplication", operationResult);                
                break;
            case 4:
                if (numbers["rightNumber"] > 0)
                   SaveCalculation("Division", operationResult);
                else
                    SaveCalculation("Division");                
                break;
        };

        _operationResultRepository.Save();

        Console.WriteLine($"Result: {result}");
        Console.Clear();

        return result;
    }

    public void SaveCalculation(string operationName)
    {
        Operation operation = _mathematicOperation.Create(operationName);
        
        _operationRepository.Create(operation);
        _operationRepository.Save();
    }

    public void SaveCalculation(string operationName, OperationResult operationResult)
    {
        Operation operation = _mathematicOperation.Create(operationName);

        _operationRepository.Create(operation);
        _operationRepository.Save();

        operationResult.OperationId = operation.Id;
        operationResult.Operation = operation;

        _operationResultRepository.Create(operationResult);
        _operationResultRepository.Save();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Delay(100, stoppingToken);
        Run();
    }
}