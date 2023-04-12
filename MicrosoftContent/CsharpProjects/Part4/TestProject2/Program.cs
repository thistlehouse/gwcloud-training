// string pangram = "The quick brown fox jumps over the lazy dog";

// char[] pangramChar = pangram.ToCharArray();

// Array.Reverse(pangramChar);

// string newPangram = new string(pangramChar);

// string[] newPangramWords = newPangram.Split(" ");

// Array.Reverse(newPangramWords);

// string result = String.Join(" ", newPangramWords);

// Console.WriteLine(result);

// Console.ReadLine();

/*

*/

string orderStream = "B123,C234,A345,C15,B177,G3003,C235,B179";

string[] orderStreamArray = orderStream.Split(",");

foreach(string order in orderStreamArray)
{
    char[] orderId = order.ToCharArray();

    if (orderId.Length == 4)
        Console.WriteLine(order);
    else
        Console.WriteLine(order + "\t- Error");
}