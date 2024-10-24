using Microsoft.Azure.Cosmos;

namespace Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Generator.DataSource;

public interface ICosmosContext
{
    Task SeedDataAsync<T>(string connectionString, string databaseName, ContainerProperties containerProperties, IEnumerable<T> items, Action<string> onCreated);
}