namespace CosmicWorks.Generator;

public interface ICosmosContext
{
    Task SeedDataAsync<T>(string databaseName, string containerName, IEnumerable<T> items, params string[] partitionKeyPaths);
}