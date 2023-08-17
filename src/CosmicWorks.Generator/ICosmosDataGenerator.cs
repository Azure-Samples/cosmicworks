namespace CosmicWorks.Generator;

public interface ICosmosDataGenerator<T>
{
    Task GenerateAsync(string databaseName, string containerName, int count);
}