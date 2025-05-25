// Copyright (c) Microsoft Corporation. All rights reserved.
using Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Data;

namespace Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Tool.Settings;

/// <summary>
/// Settings for generating <see cref="Employee"/>  data in the CosmicWorks tool.
/// </summary>
internal sealed class GenerateEmployeesSettings : GenerateSettings
{
    /// <summary>
    /// Optional.
    /// The name of the container to use.
    /// Defaults to 'employees'.
    /// </summary>
    [Description("Optional. The name of the container to use. Defaults to 'employees'.")]
    [CommandOption("-c|--container-name <CONTAINER_NAME>")]
    public override string ContainerName { get; init; } = "employees";

    /// <inheritdoc />
    public override ValidationResult Validate()
    {
        ValidationResult result = base.Validate();
        if (!result.Successful)
        {
            return result;
        }

        string? error = Quantity switch
        {
            < 1 => "The quantity must be at least 1.",
            > EmployeesDataSource.MaxEmployeesCount => $"The quantity must be less than {EmployeesDataSource.MaxEmployeesCount + 1:N0}.",
            _ => null,
        };

        return error is not null ? ValidationResult.Error(error) : ValidationResult.Success();
    }
}