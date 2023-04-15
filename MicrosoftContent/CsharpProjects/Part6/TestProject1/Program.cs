int x = 5;

ChangeValue(x);

Console.WriteLine(x);

int ChangeValue(int value) 
{
    return value = 10;
}