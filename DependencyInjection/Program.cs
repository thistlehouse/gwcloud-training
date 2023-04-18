
using System;
using DI;
using DI.Services;

using Microsoft.Extensions.DependencyInjection;

var serviceProvider = new ServiceCollection()
    .AddSingleton<ICalculatorService, CalculatorService>()
    .AddSingleton<IDisplayMenuService, DisplayMenuService>()
    .BuildServiceProvider();

var calculatorService = serviceProvider.GetRequiredService<ICalculatorService>();
var displayMenu = serviceProvider.GetRequiredService<IDisplayMenuService>();

Calculator calculator = new Calculator(calculatorService, displayMenu);

calculator.Run();