using CosmicWorks.Tool.Commands;
using CosmicWorks.Tool.Interfaces;
using CosmicWorks.Tool.Services;
using Spectre.Console.Cli;
using Spectre.Console;
using Microsoft.Extensions.DependencyInjection;
using CosmicWorks.Tool.Models;

var registrations = new ServiceCollection();

registrations.AddSingleton<IGeneratorService<Product>, BogusProductGeneratorService>();

var registrar = new ServiceCollectionRegistrar(registrations);

CommandApp app = new(registrar);

app.SetDefaultCommand<GenerateDataCommand>()
    .WithDescription("Generate fictitious data for Azure Cosmos DB for NoSQL.");

app.Configure(config =>
{
    config.SetApplicationName("cosmicworks");
    config.AddExample(new[] { "--emulator" });
    config.AddExample(new[] { "--emulator", "--number-of-items", "100" });
    config.AddExample(new[] { "--connection-string", "<API-NOSQL-CONNECTION-STRING>" });
    config.AddExample(new[] { "--connection-string", "<API-NOSQL-CONNECTION-STRING>", "--number-of-items", "500" });
});

return await app.RunAsync(args);