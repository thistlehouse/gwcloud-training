
// Random coinFlip = new Random();

// string headsOrTails = coinFlip.Next(1, 3) == 1 ? "heads" : "tails";

// Console.WriteLine(headsOrTails);

string permission = "Admin|Manager";
int level = 55;

bool isAdmin = permission.Contains("Admin");
bool isManager = permission.Contains("Manager");

if (isAdmin)
{
    if (level > 55)
        Console.WriteLine("Welcome, super Admin user.");
    
    else     
        Console.WriteLine("Welcome, Admin user.");      
}
else if (isManager)
{
    if (level >= 20)
        Console.WriteLine("Contact an Admin for access.");

    else
        Console.WriteLine("You do not have sufficient privileges");        
}
else
    Console.WriteLine("You do not have sufficient privileges.");

