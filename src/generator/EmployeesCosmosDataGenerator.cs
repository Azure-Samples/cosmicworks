using CosmicWorks.Data;
using CosmicWorks.Models;
using CosmicWorks.Generator.DataSource;
using Microsoft.Azure.Cosmos;
using System.Collections.ObjectModel;

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
                new CompositePath() { Path = "/company", Order = CompositePathSortOrder.Ascending },
                new CompositePath() { Path = "/department", Order = CompositePathSortOrder.Ascending }
            }
        );
        indexingPolicy.CompositeIndexes.Add(
            new Collection<CompositePath>
            {
                new CompositePath() { Path = "/company", Order = CompositePathSortOrder.Ascending },
                new CompositePath() { Path = "/department", Order = CompositePathSortOrder.Ascending },
                new CompositePath() { Path = "/territory", Order = CompositePathSortOrder.Ascending }
            }
        );

        ContainerProperties containerProperties = disableHierarchicalPartitionKeys ?
            new ContainerProperties(
                id: containerName,
                partitionKeyPath: "/company"
            ) : new ContainerProperties(
                id: containerName,
                partitionKeyPaths: new List<string> { "/company", "/department", "/territory" }
            );
        containerProperties.IndexingPolicy = indexingPolicy;

        await _cosmosContext.SeedDataAsync<Employee>(
            connectionString,
            databaseName,
            containerProperties,
            seedItems,
            onItemCreate
        );
    }
}
