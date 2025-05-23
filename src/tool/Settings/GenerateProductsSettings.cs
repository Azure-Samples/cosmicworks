// Copyright (c) Microsoft Corporation. All rights reserved.
namespace Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Tool.Settings;

/// <summary>
/// Settings for generating <see cref="Product"/>  data in the CosmicWorks tool.
/// </summary>
internal sealed class GenerateProductsSettings : GenerateSettings
{
    /// <summary>
    /// Optional.
    /// The name of the container to use.
    /// Defaults to 'products'.
    /// </summary>
    [Description("Optional. The name of the container to use. Defaults to 'products'.")]
    [CommandOption("-c|--container-name <CONTAINER_NAME>")]
    public override string ContainerName { get; init; } = "products";

    /// <inheritdoc />
    public override ValidationResult Validate()
    {
        ValidationResult result = base.Validate();
        if (!result.Successful)
        {
            return result;
        }

        return Quantity switch
        {
            < 1 => ValidationResult.Error("The quantity must be at least 1."),
            > 1759 => ValidationResult.Error("The quantity must be less than 1760."),
            _ => ValidationResult.Success(),
        };
    }
}