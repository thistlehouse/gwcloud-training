namespace DI.Services;

public class DisplayMenuService : IDisplayMenuService
{
    public void DisplayMainMenu()
    {
        Console.WriteLine("========== Calculator ==========");
        Console.WriteLine("1 -  Add");
        Console.WriteLine("2 - Subtrac");
        Console.WriteLine("3 - Multiply");
        Console.WriteLine("4 - Divide");
        Console.WriteLine("0 - Exit");      
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