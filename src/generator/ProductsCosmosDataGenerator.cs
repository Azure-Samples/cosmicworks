using Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Data;
using Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Models;
using Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Generator.DataSource;
using Microsoft.Azure.Cosmos;
using System.Collections.ObjectModel;

namespace Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Generator;

public class ProductsCosmosDataGenerator : ICosmosDataGenerator<Product>
{
    private readonly ICosmosContext _cosmosContext;
    private readonly IDataSource<Product> _dataSource;

    public ProductsCosmosDataGenerator(ICosmosContext cosmosContext, IDataSource<Product> dataSource)
    {
        _cosmosContext = cosmosContext;
        _dataSource = dataSource;
    }

    public async Task GenerateAsync(string connectionString, string databaseName, string containerName, int count, bool disableHierarchicalPartitionKeys, Action<string> onItemCreate)
    {
        var seedItems = _dataSource.GetItems(count);

        IndexingPolicy indexingPolicy = new()
        {
            Automatic = true,
            IndexingMode = IndexingMode.Consistent
        };
        indexingPolicy.IncludedPaths.Add(new IncludedPath { Path = "/*" });
        indexingPolicy.ExcludedPaths.Add(new ExcludedPath { Path = "/\"_etag\"/?" });
        indexingPolicy.CompositeIndexes.Add(
            new Collection<CompositePath>
            {
                new CompositePath() { Path = "/category/name", Order = CompositePathSortOrder.Ascending },
                new CompositePath() { Path = "/category/subCategory/name", Order = CompositePathSortOrder.Ascending }
            }
        );

        ContainerProperties containerProperties = disableHierarchicalPartitionKeys ?
            new ContainerProperties(
                id: containerName,
                partitionKeyPath: "/category/name"
            ) : new ContainerProperties(
                id: containerName,
                partitionKeyPaths: new List<string> { "/category/name", "/category/subCategory/name" }
            );
        containerProperties.IndexingPolicy = indexingPolicy;

        await _cosmosContext.SeedDataAsync<Product>(
            connectionString,
            databaseName,
            containerProperties,
            seedItems,
            onItemCreate
        );
    }
}
