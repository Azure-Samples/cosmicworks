namespace CosmicWorks.Generator;

public interface ICosmosDataGenerator<T>
{
    Task GenerateAsync(string connectionString, string databaseName, string containerName, int count, bool disableHierarchicalPartitionKeys, Action<string> onItemCreate);
}