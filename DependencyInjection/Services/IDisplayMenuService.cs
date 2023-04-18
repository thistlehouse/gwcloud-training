namespace DI.Services;

public interface IDisplayMenuService
{
    void DisplayMainMenu();
    Dictionary<string, decimal> GetNumbers(bool canUseZero);
    void PressAnyKeyToContinue();
}