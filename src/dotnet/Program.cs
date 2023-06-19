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

return await new CommandApp<GenerateDataCommand>(registrar)
    .RunAsync(args);