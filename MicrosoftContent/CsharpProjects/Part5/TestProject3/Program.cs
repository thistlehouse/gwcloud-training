Random random = new Random();

Console.WriteLine("Would you like to play? (Y/N)");
if (ShouldPlay()) 
{
    PlayGame();
}

void PlayGame() 
{
    var play = true;

    while (play) 
    {
        var target = random.Next(1, 5);
        var roll = random.Next(1, 7);

        Console.WriteLine($"Roll a number greater than {target} to win!");
        Console.WriteLine($"You rolled a {roll}");
        Console.WriteLine(WinOrLose(target, roll));
        Console.WriteLine("\nPlay again? (Y/N)");

        play = ShouldPlay();
    }
}

bool ShouldPlay()
{
    string input = "";
    bool exitOrNot = false;

    do
    {
        input = Console.ReadLine();

    } while (input == "");

    switch(input)
    {
        case "y":
        case "Y":
            exitOrNot = true;
            break;
        case "n":
        case "N":
            exitOrNot = false;
            break;
    }

    return exitOrNot;
}

string WinOrLose(int target, int roll)
{
    return (roll > target) ? "You Win!" : "You Lost!";
}