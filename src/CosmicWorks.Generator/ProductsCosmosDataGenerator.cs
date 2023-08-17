using CosmicWorks.Data;
using CosmicWorks.Data.Models;
using CosmicWorks.Generator.DataSource;

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

    public async Task GenerateAsync(string connectionString, string databaseName, string containerName, int count)
    {
        var seedItems = await _dataSource.GetItemsAsync(count);
        await _cosmosContext.SeedDataAsync<Product>(
            connectionString,
            databaseName,
            containerName,
            seedItems,
            "/category/name",
            "/category/subCategory/name"
        );
    }
}
