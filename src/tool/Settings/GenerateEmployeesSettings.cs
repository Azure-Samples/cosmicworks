// Copyright (c) Microsoft Corporation. All rights reserved.
using Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Data;

namespace Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Tool.Settings;

/// <summary>
/// Settings for generating <see cref="Employee"/>  data in the CosmicWorks tool.
/// </summary>
internal sealed class GenerateEmployeesSettings : GenerateSettings, IConnectionConfiguration, IItemSettings<Employee>
{
    /// <summary>
    /// Optional.
    /// Use the Azure Cosmos DB Emulator.
    /// </summary>
    [Description("Optional. Use the Azure Cosmos DB Emulator.")]
    [CommandOption("-e|--emulator <EMULATOR>")]
    public bool Emulator { get; init; } = false;

    /// <summary>
    /// Optional.
    /// The endpoint to the Azure Cosmos DB for NoSQL account.
    /// This option is preferred and uses role-based access control (RBAC) and Microsoft Entra to authenticate.
    /// The --connection-string option is not recommended.
    /// </summary>
    [Description("The endpoint to the Azure Cosmos DB for NoSQL account.")]
    [CommandOption("-n|--endpoint <ENDPOINT>")]
    public string? Endpoint { get; init; }

    /// <summary>
    /// Optional.
    /// The number of items to generate.
    /// Defaults to the maximum number of items for the entity type.
    /// </summary>
    [Description("Optional. The number of items to generate. Defaults to the maximum number of items for the entity type.")]
    [CommandOption("-q|--quantity <QUANTITY>")]
    public int? Quantity { get; init; }

    /// <summary>
    /// Optional.
    /// The name of the database to use.
    /// Defaults to 'cosmicworks'.
    /// </summary>
    [Description("Optional. The name of the database to use. Defaults to 'cosmicworks'.")]
    [CommandOption("-d|--database-name <DATABASE_NAME>")]
    public string DatabaseName { get; init; } = "cosmicworks";

    /// <summary>
    /// Optional.
    /// The name of the container to use.
    /// Defaults to 'employees'.
    /// </summary>
    [Description("Optional. The name of the container to use. Defaults to 'employees'.")]
    [CommandOption("-c|--container-name <CONTAINER_NAME>")]
    public string ContainerName { get; init; } = "employees";

    /// <summary>
    /// Optional [NOT RECOMMENDED].
    /// The connection string to the Azure Cosmos DB for NoSQL account using a resource owner password credential (ROPC).
    /// The --endpoint option is preferred.
    /// </summary>
    [Description("Optional [NOT RECOMMENDED]. The connection string to the Azure Cosmos DB for NoSQL account using a resource owner password credential (ROPC). The --endpoint option is preferred.")]
    [CommandOption("--connection-string <CONNECTION_STRING>")]
    public string? ConnectionString { get; init; }

    /// <summary>
    /// Optional.
    /// Hides the credentials when rendering to output.
    /// It is recommended to use this option when using the --connection-string option.
    /// Defaults to false.
    /// </summary>
    [Description("Optional. Hides the credentials. It is recommended to use this option when using the --connection-string option. Defaults to false.")]
    [CommandOption("--hide-credentials <HIDE_CREDENTIALS>")]
    public bool? HideCredentials { get; init; } = false;

    /// <summary>
    /// Optional.
    /// Disables ANSI and color formatting for console output.
    /// This setting is useful for redirecting output to a file, log, or other output stream that doesn't support ANSI or color formatting.
    /// Defaults to false.
    /// </summary>
    [Description("Optional. Disables ANSI and color formatting for console output. This setting is useful for redirecting output to a file, log, or other output stream that doesn't support ANSI or color formatting. Defaults to false.")]
    [CommandOption("--disable-formatting <DISABLE_FORMATTING>")]
    public bool? DisableFormatting { get; init; } = false;

    /// <summary>
    /// Optional.
    /// Disables hierarchical partition keys and uses only the first partition key value.
    /// Defaults to false.
    /// </summary>
    [Description("Optional. Disables hierarchical partition keys and uses only the first partition key value. Defaults to false.")]
    [CommandOption("--disable-hierarchical-partition-keys")]
    public bool? DisableHierarchicalPartitionKeys { get; init; } = false;

    /// <inheritdoc />
    public override ValidationResult Validate()
    {
        return (Endpoint, ConnectionString, Emulator, Quantity) switch
        {
            (null, null, false, _) => ValidationResult.Error("You must provide a connection string, an endpoint, or use the emulator."),
            (_, not null, true, _) => ValidationResult.Error("You can't provide a connection string when using the emulator."),
            (not null, _, true, _) => ValidationResult.Error("You can't provide an endpoint when using the emulator."),
            (not null, not null, _, _) => ValidationResult.Error("You can't provide both an endpoint and a connection string."),
            ("", _, _, _) => ValidationResult.Error("You must provide a valid value for the endpoint."),
            (_, "", _, _) => ValidationResult.Error("You must provide a valid value for the connection string."),
            (_, _, _, < 1) => ValidationResult.Error("The quantity must be at least 1."),
            (_, _, _, > EmployeesDataSource.MaxEmployeesCount) => ValidationResult.Error($"The quantity must be less than {EmployeesDataSource.MaxEmployeesCount + 1:N0}."),
            _ => ValidationResult.Success(),
        };
    }
}