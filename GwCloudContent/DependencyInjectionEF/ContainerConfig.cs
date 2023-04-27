using DI.Persistence;
using DI.Services;
using DIEF.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DIEF
{
    public static class ContainerConfig
    {
        public static IHost Configure()
        {
            var builder =  Host.CreateDefaultBuilder();

            builder.ConfigureServices(services => 
                services.AddDbContext<DIDbContext>()
                .AddHostedService<Calculator>()
                .AddSingleton<IDisplayMenuService, DisplayMenuService>()
                .AddScoped<ICalculatorService, CalculatorService>()
                .AddTransient<ICalculator, Calculator>()
                .AddTransient<IInputReaderService, InputReaderService>()
                .AddScoped<IMathematicOperation, MathematicOperation>()
                .AddScoped<IOperationRepository, OperationRepository>()
                .AddScoped<IOperationResultRepository, OperationResultRepository>()
            );

            return builder.Build();
        }
    }
}