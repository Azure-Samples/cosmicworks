using CosmicWorks.Data;
using CosmicWorks.Data.Models;
using CosmicWorks.Generator.DataSource;

namespace CosmicWorks.Generator;

public class EmployeesCosmosDataGenerator : ICosmosDataGenerator<Employee>
{
    private readonly ICosmosContext _cosmosContext;
    private readonly IDataSource<Employee> _dataSource;

    public EmployeesCosmosDataGenerator(ICosmosContext cosmosContext, IDataSource<Employee> dataSource)
    {
        _cosmosContext = cosmosContext;
        _dataSource = dataSource;
    }

    public async Task GenerateAsync(string connectionString, string databaseName, string containerName, int count, Action<string> onItemCreate)
    {
        var seedItems = await _dataSource.GetItemsAsync(count);
        await _cosmosContext.SeedDataAsync<Employee>(
            connectionString,
            databaseName,
            containerName,
            seedItems,
            onItemCreate,
            "/company",
            "/department",
            "/territory"
        );
    }
}
