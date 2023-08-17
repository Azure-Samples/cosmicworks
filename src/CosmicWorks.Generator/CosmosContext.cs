using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;

namespace CosmicWorks.Generator;

internal sealed class CosmosContext : ICosmosContext
{
    private readonly string _connectionString;

    public CosmosContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task SeedDataAsync<T>(string databaseName, string containerName, IEnumerable<T> items, params string[] partitionKeyPaths)
    {
        using CosmosClient client = new CosmosClientBuilder(_connectionString)
            .WithSerializerOptions(new CosmosSerializationOptions()
            {
                PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase,
            })
            .WithThrottlingRetryOptions(TimeSpan.FromSeconds(30), 30)
            .Build();

        Database database = await client.CreateDatabaseIfNotExistsAsync(
            id: databaseName,
            throughput: 400
        );

        Container container = await database.CreateContainerIfNotExistsAsync(
            new ContainerProperties(
                id: containerName,
                partitionKeyPaths: partitionKeyPaths.ToList<string>()
            )
        );

        await database.ReplaceThroughputAsync(4000);

        List<Task> tasks = new();
        foreach (var item in items)
        {
            tasks.Add(
                container.UpsertItemAsync(item)
                    .ContinueWith(r =>
                        {
                            if (r.IsCompletedSuccessfully)
                            {
                                Console.WriteLine($"[NEW {nameof(T).ToUpperInvariant()}]\t{r.Result.Resource}");
                            }
                        }
                    )
            );
        }
        await Task.WhenAll(tasks);

        await database.ReplaceThroughputAsync(400);
    }
}