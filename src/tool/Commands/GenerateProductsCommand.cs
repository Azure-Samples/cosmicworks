// Copyright (c) Microsoft Corporation. All rights reserved.
namespace Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Tool.Commands;

internal sealed class GenerateProductsCommand(
    ICosmosDataGenerator<Product> productGenerator
) : AsyncCommand<GenerateProductsSettings>
{
    /// <inheritdoc />
    public override async Task<int> ExecuteAsync(CommandContext context, GenerateProductsSettings settings)
    {
        IAnsiConsole console = settings.GetConsole();

        console.WriteConfiguration(settings);

        ConnectionOptions options = settings.ToConnectionOptions();

        await productGenerator.GenerateAsync(
            options: options,
            databaseName: settings.DatabaseName,
            containerName: settings.ContainerName,
            count: settings.Quantity,
            disableHierarchicalPartitionKeys: settings.Emulator || (settings.DisableHierarchicalPartitionKeys ?? false),
            onItemCreate: console.WriteItem
        );

        return 0;
    }
}