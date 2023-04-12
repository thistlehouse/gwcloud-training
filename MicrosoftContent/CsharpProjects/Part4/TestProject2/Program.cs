string pangram = "The quick brown fox jumps over the lazy dog";

char[] pangramChar = pangram.ToCharArray();

Array.Reverse(pangramChar);

string newPangram = new string(pangramChar);

string[] newPangramWords = newPangram.Split(" ");

Array.Reverse(newPangramWords);

string result = String.Join(" ", newPangramWords);

Console.WriteLine(result);

Console.ReadLine();