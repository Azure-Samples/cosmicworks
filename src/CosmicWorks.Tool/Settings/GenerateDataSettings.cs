using Spectre.Console;
using Spectre.Console.Cli;
using System.ComponentModel;

namespace CosmicWorks.Tool.Settings;

public sealed class GenerateDataSettings : CommandSettings
{
    [Description("The connection string to the Azure Cosmos DB for NoSQL account.")]
    [CommandOption("-c|--connection-string <CONNECTION_STRING>")]
    public string? ConnectionString { get; init; }

    [Description("Use the Azure Cosmos DB Emulator.")]
    [CommandOption("-e|--emulator <EMULATOR>")]
    public bool Emulator { get; init; } = false;

    [Description("The number of products to generate.")]
    [CommandOption("--number-of-products <NUMBER_OF_PRODUCTS>")]
    public int NumberOfProducts { get; init; } = 1000;

    [Description("The number of employees to generate.")]
    [CommandOption("--number-of-employees <NUMBER_OF_EMPLOYEES>")]
    public int NumberOfEmployees { get; init; } = 200;

    public override ValidationResult Validate()
    {
        return (NumberOfProducts, NumberOfEmployees) switch
        {
            ( > 1500, _) => ValidationResult.Error("You can't generate more than 1500 products."),
            (_, > 200) => ValidationResult.Error("You can't generate more than 200 employees."),
            ( <= 0, <= 0) => ValidationResult.Error("You must generate at least one product or employee."),
            _ => ValidationResult.Success(),
        };
    }
}