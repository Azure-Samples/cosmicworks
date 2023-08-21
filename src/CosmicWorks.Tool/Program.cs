using CosmicWorks.Data;
using CosmicWorks.Data.Models;
using CosmicWorks.Generator;
using CosmicWorks.Generator.DataSource;
using CosmicWorks.Tool.Commands;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Spectre.Console.Cli;

var registrations = new ServiceCollection();

registrations.AddSingleton<ILoggerFactory, LoggerFactory>();
registrations.AddSingleton(typeof(ILogger<>), typeof(Logger<>));
registrations.AddSingleton<ICosmosContext, CosmosContext>();
registrations.AddSingleton<IDataSource<Product>, ProductsDataSource>();
registrations.AddSingleton<IDataSource<Employee>, EmployeesDataSource>();
registrations.AddSingleton<ICosmosDataGenerator<Product>, ProductsCosmosDataGenerator>();
registrations.AddSingleton<ICosmosDataGenerator<Employee>, EmployeesCosmosDataGenerator>();

var registrar = new ServiceCollectionRegistrar(registrations);

CommandApp app = new(registrar);

app.SetDefaultCommand<GenerateDataCommand>()
    .WithDescription("Generate and seed fictitious data in an Azure Cosmos DB for NoSQL account.");

app.Configure(config =>
{
    config.SetApplicationName("cosmicworks");
    config.AddExample(new[] { "--help" });
    config.AddExample(new[] { "--version" });
    config.AddExample(new[] { "--emulator" });
    config.AddExample(new[] { "--emulator", "--number-of-employees", "0" });
    config.AddExample(new[] { "--emulator", "--number-of-products", "0" });
    config.AddExample(new[] { "--connection-string", "\"<API-NOSQL-CONNECTION-STRING>\"" });
    config.AddExample(new[] { "--connection-string", "\"<API-NOSQL-CONNECTION-STRING>\"", "--number-of-employees", "100" });
    config.AddExample(new[] { "--connection-string", "\"<API-NOSQL-CONNECTION-STRING>\"", "--number-of-products", "500" });
    config.AddExample(new[] { "--connection-string", "\"<API-NOSQL-CONNECTION-STRING>\"", "--number-of-employees", "200", "--number-of-products", "1000" });
});

return await app.RunAsync(args);