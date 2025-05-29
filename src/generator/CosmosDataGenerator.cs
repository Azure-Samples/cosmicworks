// Copyright (c) Microsoft Corporation. All rights reserved.
namespace Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Generator;

/// <summary>
/// A data generator for Azure Cosmos DB for NoSQL that generates items of a generic type.
/// </summary>
public class CosmosDataGenerator<T>(
    IDataSource<T> dataSource,
    ICosmosDataService<T> cosmosDataService
) : ICosmosDataGenerator<T> where T : IItem
{
    /// <inheritdoc />
    public async Task GenerateAsync(ConnectionOptions options, string databaseName, string containerName, int? count = null, bool disableHierarchicalPartitionKeys = false, Action<T>? onItemCreate = null)
    {
        ArgumentNullException.ThrowIfNull(options);

        onItemCreate ??= _ => { };

        IReadOnlyList<T> seedItems = dataSource.GetItems(count);

        if (options.Type == ConnectionType.ResourceOwnerPasswordCredential)
        {
            string[] partitionKeys = [.. seedItems[0].PartitionKeys];

            await cosmosDataService.ProvisionResourcesAsync(
                options,
                databaseName,
                containerName,
                partitionKeys,
                disableHierarchicalPartitionKeys
            );
        }

        await cosmosDataService.SeedDataAsync(
            options,
            databaseName,
            containerName,
            seedItems,
            onItemCreate
        );
    }
}