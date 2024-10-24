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
    public int NumberOfProducts { get; init; } = 1759;

    [Description("The number of employees to generate.")]
    [CommandOption("--number-of-employees <NUMBER_OF_EMPLOYEES>")]
    public int NumberOfEmployees { get; init; } = 234;

    [Description("Gets the version of the tool.")]
    [CommandOption("-v|--version <VERSION>")]
    public bool RenderVersion { get; init; } = false;

    [Description("Hides the credentials.")]
    [CommandOption("--hide-credentials <HIDE_CREDENTIALS>", IsHidden = true)]
    public bool? HideCredentials { get; init; } = false;

    [Description("Disables ANSI and color formatting for console output.")]
    [CommandOption("--disable-formatting <DISABLE_FORMATTING>", IsHidden = true)]
    public bool? DisableFormatting { get; init; } = false;

    [Description("Disables hierarchical partition keys and uses only the first partition key value.")]
    [CommandOption("--disable-hierarchical-partition-keys")]
    public bool? DisableHierarchicalPartitionKeys { get; init; } = false;

    public override ValidationResult Validate()
    {
        return (NumberOfProducts, NumberOfEmployees) switch
        {
            ( > 1759, _) => ValidationResult.Error("You can't generate more than 1,759 products."),
            (_, > 234) => ValidationResult.Error("You can't generate more than 234 employees."),
            ( <= 0, <= 0) => ValidationResult.Error("You must generate at least one product or employee."),
            _ => ValidationResult.Success(),
        };
    }
}