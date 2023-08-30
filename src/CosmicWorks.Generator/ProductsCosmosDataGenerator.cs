using CosmicWorks.Data;
using CosmicWorks.Data.Models;
using CosmicWorks.Generator.DataSource;
using Microsoft.Azure.Cosmos;
using System.Collections.ObjectModel;

namespace CosmicWorks.Generator;

public class ProductsCosmosDataGenerator : ICosmosDataGenerator<Product>
{
    private readonly ICosmosContext _cosmosContext;
    private readonly IDataSource<Product> _dataSource;

    public ProductsCosmosDataGenerator(ICosmosContext cosmosContext, IDataSource<Product> dataSource)
    {
        _cosmosContext = cosmosContext;
        _dataSource = dataSource;
    }

    public async Task GenerateAsync(string connectionString, string databaseName, string containerName, int count, Action<string> onItemCreate)
    {
        var seedItems = await _dataSource.GetItemsAsync(count);

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

        ContainerProperties containerProperties = new(
                id: containerName,
                partitionKeyPaths: new List<string> { "/category/name", "/category/subCategory/name" }
        )
        {
            IndexingPolicy = indexingPolicy
        };

        await _cosmosContext.SeedDataAsync<Product>(
            connectionString,
            databaseName,
            containerProperties,
            seedItems,
            onItemCreate
        );
    }
}
