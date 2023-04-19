using DI.Domain;

namespace DI.Services;

public interface IDisplayMenuService
{
    void DisplayMainMenu();
    List<Operation> CreateMenu();
}