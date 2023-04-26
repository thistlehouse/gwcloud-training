const string input = "<div><h2>Widgets &trade;</h2><span>5000</span></div>";

string quantity = "";
string output = "";

// Your work here
string spanOpenTag = "<span>";
string spanCloseTag = "</span>";
string divOpenTag = "<div>";
string divCloseTag = "</div>";

int openSpan = input.IndexOf(spanOpenTag);
int closeSpan = input.IndexOf(spanCloseTag);
int openDiv = input.IndexOf(divOpenTag);

openSpan += spanOpenTag.Length;

int spanLength = (closeSpan - openSpan);

quantity = input.Substring(openSpan, spanLength);

output = input.Replace("&trade;", "&reg;");
output = output.Remove(openDiv, divOpenTag.Length);

int closeDiv = output.IndexOf(divCloseTag);

output = output.Remove(closeDiv, divCloseTag.Length);

Console.WriteLine(quantity);
Console.WriteLine(output);