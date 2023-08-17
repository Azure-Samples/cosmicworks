using System.ComponentModel;
using Spectre.Console;
using Spectre.Console.Cli;

namespace CosmicWorks.Tool.Settings;

public sealed class GenerateDataSettings : CommandSettings
{
    [Description("The connection string to the Azure Cosmos DB for NoSQL account.")]
    [CommandOption("-c|--connection-string <CONNECTION_STRING>")]
    public string? ConnectionString { get; init; }

    [Description("Use the Azure Cosmos DB Emulator.")]
    [CommandOption("-e|--emulator <EMULATOR>")]
    public bool Emulator { get; init; } = false;

    [Description("The number of items to generate.")]
    [CommandOption("-n|--number-of-items <NUMBER_OF_ITEMS>")]
    public int NumberOfItems { get; init; } = 1000;

    public override ValidationResult Validate()
    {
        return NumberOfItems switch
        {
            > 5000 => ValidationResult.Error("You can't generate more than 5000 items"),
            <= 0 => ValidationResult.Error("You must generate at least one item"),
            _ => ValidationResult.Success(),
        };
    }
}