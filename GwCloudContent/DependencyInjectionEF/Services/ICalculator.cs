using DI.Services;

public interface ICalculator
{
    void Run();
    decimal PerformCalculation(int menuOptionIndex);
}

