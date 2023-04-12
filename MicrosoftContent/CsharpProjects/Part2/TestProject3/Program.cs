string[] fraudulentOrders = 
{
    "B123",
    "C234",
    "A345",
    "C15",
    "B177",
    "G3003",
    "C235",
    "B179",
};

foreach (string fraudulentOrder in fraudulentOrders)
{
    if (fraudulentOrder.StartsWith("B"))
        Console.WriteLine("Fraudulent orders with letter B: {0}", fraudulentOrder);
}