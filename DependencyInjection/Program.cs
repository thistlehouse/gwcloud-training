
using System;
using DI.Domain;
using DI.Services;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var serviceProvider = new ServiceCollection()
    .AddSingleton<ICalculatorService, CalculatorService>()
    .AddSingleton<IDisplayMenuService, DisplayMenuService>()
    .AddScoped<IMathematicOperation, MathematicOperation>()
    .AddSingleton<ICalculator, Calculator>()
    .BuildServiceProvider();

var calculator = serviceProvider.GetService<ICalculator>();

calculator.Run();

// using IHost host = CreateHostBuilder(args).Build();

// var calculator = host.Services.GetService<ICalculator>();

// await host.RunAsync();

// static IHostBuilder CreateHostBuilder(string[] args) =>
//     Host.CreateDefaultBuilder(args)
//         .ConfigureServices((_, services) =>
//             services.AddSingleton<ICalculatorService, CalculatorService>()
//                     .AddSingleton<IDisplayMenuService, DisplayMenuService>()
//                     .AddSingleton<ICalculator, Calculator>()
//                     .AddScoped<IMathematicOperation, MathematicOperation>());

//                     _ = host.Services.GetService<ICalculator>();

// calculator.Run();

// Teremos uma interface com o contrato dos metodos acima, cada metodo recebe dois valores decimais
// Teremos uma classe que herda a interface e faz a implementação dos metodos
// Teremos uma classe que executa a ação desejada pelo usuario

