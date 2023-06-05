using PaymentProcessor.ContainerConfiguration;

using var app = ContainerConfiguration.Configure();

app.StartAsync();
Console.WriteLine("\n# Payment Processing in running...");

Console.ReadLine();

app.StopAsync();

// app.Dispose();