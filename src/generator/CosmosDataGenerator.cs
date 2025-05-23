// Copyright (c) Microsoft Corporation. All rights reserved.
namespace Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Generator;

/// <summary>
/// A data generator for Azure Cosmos DB for NoSQL that generates items of a generic type.
/// </summary>
public class CosmosDataGenerator<T>(
    ICosmosDataService<T> cosmosContext,
    IDataSource<T> dataSource) : ICosmosDataGenerator<T> where T : IItem
{
    /// <inheritdoc />
    public async Task GenerateAsync(ConnectionOptions options, string databaseName, string containerName, int? count, bool disableHierarchicalPartitionKeys, Action<T> onItemCreate)
    {
        ArgumentNullException.ThrowIfNull(options);

        IReadOnlyList<T> seedItems = count is null ? dataSource.GetItems() : dataSource.GetItems(count.Value);

        if (options.Type == ConnectionType.ResourceOwnerPasswordCredential)
        {
            string[] partitionKeys = [.. seedItems[0].PartitionKeys];

            await cosmosContext.ProvisionResourcesAsync(
                options,
                databaseName,
                containerName,
                partitionKeys,
                disableHierarchicalPartitionKeys
            );
        }

        await cosmosContext.SeedDataAsync(
            options,
            databaseName,
            containerName,
            seedItems,
            onItemCreate
        );
    }
}