
using System;
using DI;
using DI.Services;

using Microsoft.Extensions.DependencyInjection;

var serviceProvider = new ServiceCollection()
    .AddSingleton<ICalculatorService, CalculatorService>()
    .BuildServiceProvider();

var calculatorService = serviceProvider.GetRequiredService<ICalculatorService>();

Calculator calculator = new Calculator(calculatorService);

calculator.Run();