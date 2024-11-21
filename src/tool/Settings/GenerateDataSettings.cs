using Spectre.Console;
using Spectre.Console.Cli;
using System.ComponentModel;

namespace Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Tool.Settings;

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

    [Description("Enables role-based access control.")]
    [CommandOption("--role-based-access-control|--rbac|-r <ROLE_BASED_ACCESS_CONTROL>")]
    public bool RoleBasedAccessControl { get; init; } = false;

    [Description("The endpoint to the Azure Cosmos DB for NoSQL account.")]
    [CommandOption("-n|--endpoint <ENDPOINT>")]
    public string? Endpoint { get; init; }

    public override ValidationResult Validate()
    {
        string? error = default;

        error = (Endpoint, RoleBasedAccessControl, ConnectionString, Emulator) switch
        {
            (null, false, null, false) => "You must provide a connection string, and endpoint (for RBAC), or use the emulator.",
            (not null, false, null, false) => "You can't provide an endpoint without using role-based access control.",
            (null, true, null, false) => "You must provide an endpoint when using role-based access control.",
            (null, true, not null, false) => "You can't provide a connection string when using role-based access control.",
            (null, true, null, true) => "You can't use the emulator when using role-based access control.",
            (not null, false, not null, false) => "You can't provide both an endpoint and a connection string.",
            (null, false, not null, true) => "You can't provide a connection string when using the emulator.",
            (not null, false, null, true) => "You can't provide an endpoint when using the emulator.",
            _ => error,
        };

        error = (NumberOfProducts, NumberOfEmployees) switch
        {
            ( > 1759, _) => "You can't generate more than 1,759 products.",
            (_, > 234) => "You can't generate more than 234 employees.",
            ( <= 0, <= 0) => "You must generate at least one product or employee.",
            _ => error,
        };

        return error is not null ? ValidationResult.Error(error) : ValidationResult.Success();
    }
}