using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PaymentProcessor.Microservice.Broker;

var builder =  Host.CreateDefaultBuilder(args);

builder.ConfigureServices(services =>
    services.AddHostedService<PaymentConsumer>()
);

var app = builder.Build();

app.StartAsync();

Console.WriteLine("\n# Payment Processing in running...");
Console.ReadLine();

app.StopAsync();

