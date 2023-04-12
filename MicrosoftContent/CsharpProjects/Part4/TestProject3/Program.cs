/*
*   Microsoft Example
*/

// string paymentId = "769";
// string payeeName = "Mr. Stephen Ortega";
// string paymentAmount = "$5,000.00";

// var formattedLine = paymentId.PadRight(6);
// formattedLine += payeeName.PadRight(24);
// formattedLine += paymentAmount.PadLeft(10);

// Console.WriteLine("1234567890123456789012345678901234567890");
// Console.WriteLine(formattedLine);


/*
*   Challenge
*/

string customerName = "Ms. Barros";

string currentProduct = "Magic Yield";
int currentShares = 2975000;
decimal currentReturn = 0.1275m;
decimal currentProfit = 55000000.0m;

string newProduct = "Glorious Future";
decimal newReturn = 0.13125m;
decimal newProfit = 63000000.0m;

// Your logic here
Console.WriteLine($"Dear {customerName}");
Console.WriteLine("As a customer of our Magic Yield offering we are excited to tell you about a new financial product that would dramatically increase your return.\n");
Console.WriteLine($"Currently, you own {currentShares:N} shares at a return of {currentReturn:P}.\n");
Console.WriteLine($"Our new product, Glorious Future offers a return of {newReturn:P}.  Given your current volume, your potential profit would be {newProfit:C}.\n");
Console.WriteLine("Here's a quick comparison:\n");
Console.WriteLine("123456789012345678901234567890");

string comparisonMessage = "";

// Your logic here
comparisonMessage += currentProduct.PadRight(22);
comparisonMessage += string.Format($"{currentReturn:P}").PadRight(10);
comparisonMessage += string.Format($"{currentProfit:C}\n");
comparisonMessage += newProduct.PadRight(22);
comparisonMessage += string.Format($"{newReturn:P}").PadRight(10);
comparisonMessage += string.Format($"{newProfit:C}");

Console.WriteLine(comparisonMessage);