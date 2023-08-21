namespace CosmicWorks.Generator;

public interface ICosmosDataGenerator<T>
{
    Task GenerateAsync(string connectionString, string databaseName, string containerName, int count, Action<string> onItemCreate);
}