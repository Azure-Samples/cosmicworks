using Microsoft.Azure.Cosmos;

namespace CosmicWorks.Generator.DataSource;

public interface ICosmosContext
{
    Task SeedDataAsync<T>(string connectionString, string databaseName, ContainerProperties containerProperties, IEnumerable<T> items, Action<string> onCreated);
}