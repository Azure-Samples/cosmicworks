// Copyright (c) Microsoft Corporation. All rights reserved.
namespace Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Tool.Settings;

using System.ComponentModel;

using Spectre.Console;
using Spectre.Console.Cli;

/// <summary>
/// Settings for generating data in the CosmicWorks tool.
/// </summary>
public sealed class GenerateDataSettings : CommandSettings
{
    /// <summary>
    /// The connection string to the Azure Cosmos DB for NoSQL account.
    /// </summary>
    [Description("The connection string to the Azure Cosmos DB for NoSQL account.")]
    [CommandOption("-c|--connection-string <CONNECTION_STRING>")]
    public string? ConnectionString { get; init; }

    /// <summary>
    /// Use the Azure Cosmos DB Emulator.
    /// </summary>
    [Description("Use the Azure Cosmos DB Emulator.")]
    [CommandOption("-e|--emulator <EMULATOR>")]
    public bool Emulator { get; init; } = false;

    /// <summary>
    /// The number of products to generate.
    /// </summary>
    [Description("The number of products to generate.")]
    [CommandOption("--number-of-products <NUMBER_OF_PRODUCTS>")]
    public int NumberOfProducts { get; init; } = 1759;

    /// <summary>
    /// The number of employees to generate.
    /// </summary>
    [Description("The number of employees to generate.")]
    [CommandOption("--number-of-employees <NUMBER_OF_EMPLOYEES>")]
    public int NumberOfEmployees { get; init; } = 234;

    /// <summary>
    /// Gets the version of the tool.
    /// </summary>
    [Description("Gets the version of the tool.")]
    [CommandOption("-v|--version <VERSION>")]
    public bool RenderVersion { get; init; } = false;

    /// <summary>
    /// Hides the credentials.
    /// </summary>
    [Description("Hides the credentials.")]
    [CommandOption("--hide-credentials <HIDE_CREDENTIALS>", IsHidden = true)]
    public bool? HideCredentials { get; init; } = false;

    /// <summary>
    /// Disables ANSI and color formatting for console output.
    /// </summary>
    [Description("Disables ANSI and color formatting for console output.")]
    [CommandOption("--disable-formatting <DISABLE_FORMATTING>", IsHidden = true)]
    public bool? DisableFormatting { get; init; } = false;

    /// <summary>
    /// Disables hierarchical partition keys and uses only the first partition key value.
    /// </summary>
    [Description("Disables hierarchical partition keys and uses only the first partition key value.")]
    [CommandOption("--disable-hierarchical-partition-keys")]
    public bool? DisableHierarchicalPartitionKeys { get; init; } = false;

    /// <summary>
    /// Enables role-based access control.
    /// </summary>
    [Description("Enables role-based access control.")]
    [CommandOption("--role-based-access-control|--rbac|-r <ROLE_BASED_ACCESS_CONTROL>")]
    public bool RoleBasedAccessControl { get; init; } = false;

    /// <summary>
    /// The endpoint to the Azure Cosmos DB for NoSQL account.
    /// </summary>
    [Description("The endpoint to the Azure Cosmos DB for NoSQL account.")]
    [CommandOption("-n|--endpoint <ENDPOINT>")]
    public string? Endpoint { get; init; }

    /// <summary>
    /// Validates the settings.
    /// </summary>
    /// <returns>The validation result.</returns>
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