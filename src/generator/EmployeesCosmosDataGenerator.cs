// Copyright (c) Microsoft Corporation. All rights reserved.
namespace Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Generator;

/// <summary>
/// A data generator for Azure Cosmos DB for NoSQL that generates items of type <see cref="Employee"/>.
/// </summary>
public class EmployeesCosmosDataGenerator(
    ICosmosContext cosmosContext,
    IDataSource<Employee> dataSource) : ICosmosDataGenerator<Employee>
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
                new CompositePath() { Path = "/company", Order = CompositePathSortOrder.Ascending },
                new CompositePath() { Path = "/department", Order = CompositePathSortOrder.Ascending }
            ]
        );
        indexingPolicy.CompositeIndexes.Add(
            [
                new CompositePath() { Path = "/company", Order = CompositePathSortOrder.Ascending },
                new CompositePath() { Path = "/department", Order = CompositePathSortOrder.Ascending },
                new CompositePath() { Path = "/territory", Order = CompositePathSortOrder.Ascending }
            ]
        );

        ContainerProperties containerProperties = disableHierarchicalPartitionKeys ?
            new ContainerProperties(
                id: containerName,
                partitionKeyPath: "/company"
            ) : new ContainerProperties(
                id: containerName,
                partitionKeyPaths: ["/company", "/department", "/territory"]
            );
        containerProperties.IndexingPolicy = indexingPolicy;

        await cosmosContext.SeedDataAsync<Employee>(
            factoryOptions,
            databaseName,
            containerProperties,
            seedItems,
            onItemCreate
        );
    }
}