// Random random = new Random();

// int heroAttack = random.Next(1, 11);
// int monsterAttack = random.Next(1, 11);
// int heroHealth = 10;
// int monsterHealth = 10;

// while (true)
// {
//     if (heroHealth > 0)
//     {
//         monsterHealth -= heroAttack;
    
//         Console.WriteLine($"Monster was damaged and lost {heroAttack} health" + 
//             $" and now has {monsterHealth} health.");
//     }

//     if (monsterHealth > 0)
//     {
//         heroHealth -= monsterAttack;

//         Console.WriteLine($"Hero was damaged and lost {monsterAttack} health" + 
//             $" and now has {heroHealth} health.");
//     }

//     if (monsterHealth <= 0)
//     {
//         Console.WriteLine("Hero wins!");
        
//         break;
//     }

//     if (heroHealth <= 0)
//     {
//         Console.WriteLine("Monster wins!");

//         break;
//     }
// }

/*
* Code project 1
*
*/

// string valueInput = "";

// int numValue = 0;

// bool validNumber = false;

// Console.WriteLine("Enter an integer value between 5 and 10");

// do
// {   
//     valueInput = Console.ReadLine();

//     if (valueInput != null)
//         validNumber = int.TryParse(valueInput, out numValue);
    
//     if (validNumber == true)
//     {
//         if (numValue < 5 || numValue > 10)
//         {
//             validNumber = false;

//             Console.WriteLine($"You entered {numValue}. Please enter a number between 5 and 10");
//         }        
//     }
//     else
//     {
//         Console.WriteLine("Sorry, you entered an invalid number, please try again");
//     }
// } while (validNumber == false);

// Console.WriteLine($"Your input value ({numValue}) has been accepted.");


/*
* Code project 2
*
*/
// string[] permissions = {"Administrator", "Manager", "User"};
// string inputValue = "";

// bool isValidPermission = false;

// Console.WriteLine("Enter your role name (Administrator, Manager, or User)");

// do
// {
//     inputValue = Console.ReadLine().Trim().ToLower();

//     if (inputValue != null)
//     {
//         for (int i = 0; i < permissions.Length; i++)
//         {
//             inputValue = inputValue.ToLower();

//             string permissionToLower = permissions[i].ToLower();           

//             isValidPermission = permissionToLower.Equals(inputValue);

//             if (isValidPermission)
//                 break;
//         }

//         if (!(isValidPermission))
//             Console.WriteLine($"The role name that you entered, {inputValue} " +
//                 "is not valid. Enter your role name (Administrator, Manager, or User)");
//     }
// } while (isValidPermission == false);

// Console.WriteLine($"Your input ({inputValue}) has been accepted.");


/*
* Code project 3
*
*/
string[] myStrings = new string[2] { "I like pizza. I like roast chicken. I like salad", "I like all three of the menu choices" };
string message = "";

int periodLocation = 0;

foreach (string myString in myStrings)
{
    message = myString;
    periodLocation = message.IndexOf(".");

    string processedMessage = ""; 

    do
    {   
        if (periodLocation < 0)
            Console.WriteLine(message);
        else
        {
            processedMessage = message.Substring(0, periodLocation);                
            message = message.Remove(0, periodLocation + 1);
            message = message.TrimStart();

            Console.WriteLine(processedMessage);

            periodLocation = message.IndexOf("."); 

            if (periodLocation < 0)
                Console.WriteLine(message);
        }
        

    } while (periodLocation != -1);
}