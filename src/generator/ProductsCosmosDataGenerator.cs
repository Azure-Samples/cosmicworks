// Copyright (c) Microsoft Corporation. All rights reserved.
namespace Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Generator;

/// <summary>
/// A data generator for Azure Cosmos DB for NoSQL that generates items of type <see cref="Product"/>.
/// </summary>
public class ProductsCosmosDataGenerator(
    ICosmosContext cosmosContext,
    IDataSource<Product> dataSource) : ICosmosDataGenerator<Product>
{
    /// <inheritdoc/>
    public async Task GenerateAsync(CosmosClientBuilderFactoryOptions factoryOptions, string databaseName, string containerName, int count, bool disableHierarchicalPartitionKeys, Action<string> onItemCreate)
    {
        var seedItems = dataSource.GetItems(count);

        IndexingPolicy indexingPolicy = new()
        {
            Automatic = true,
            IndexingMode = IndexingMode.Consistent
        };
        indexingPolicy.IncludedPaths.Add(new IncludedPath { Path = "/*" });
        indexingPolicy.ExcludedPaths.Add(new ExcludedPath { Path = "/\"_etag\"/?" });
        indexingPolicy.CompositeIndexes.Add(
            [
                new CompositePath() { Path = "/category/name", Order = CompositePathSortOrder.Ascending },
                new CompositePath() { Path = "/category/subCategory/name", Order = CompositePathSortOrder.Ascending }
            ]
        );

        ContainerProperties containerProperties = disableHierarchicalPartitionKeys ?
            new ContainerProperties(
                id: containerName,
                partitionKeyPath: "/category/name"
            ) : new ContainerProperties(
                id: containerName,
                partitionKeyPaths: ["/category/name", "/category/subCategory/name"]
            );
        containerProperties.IndexingPolicy = indexingPolicy;

        await cosmosContext.SeedDataAsync<Product>(
            factoryOptions,
            databaseName,
            containerProperties,
            seedItems,
            onItemCreate
        );
    }
}