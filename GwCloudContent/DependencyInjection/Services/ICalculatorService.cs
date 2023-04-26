namespace DI.Services;

public interface ICalculatorService
{
    decimal Add(decimal x, decimal y);
    decimal Subtract(decimal x, decimal y);
    decimal Multiply(decimal x, decimal y);
    decimal Divide(decimal x, decimal y);
}