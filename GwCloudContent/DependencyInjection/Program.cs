
using System;
using DI.Domain;
using DI.Services;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder =  Host.CreateDefaultBuilder(args);

builder.ConfigureServices(services => 
    services.AddHostedService<Calculator>()
        .AddSingleton<IDisplayMenuService, DisplayMenuService>()
        .AddScoped<ICalculatorService, CalculatorService>()
        .AddScoped<ICalculator, Calculator>()
        .AddScoped<IInputReaderService, InputReaderService>()
        .AddScoped<IMathematicOperation, MathematicOperation>()
    );

using var app = builder.Build();

app.Run();