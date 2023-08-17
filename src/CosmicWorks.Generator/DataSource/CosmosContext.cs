using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;
using Console = Spectre.Console.AnsiConsole;

namespace CosmicWorks.Generator.DataSource;

public sealed class CosmosContext : ICosmosContext
{
    public async Task SeedDataAsync<T>(string connectionString, string databaseName, string containerName, IEnumerable<T> items, params string[] partitionKeyPaths)
    {
        using CosmosClient client = new CosmosClientBuilder(connectionString)
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
                partitionKeyPaths: partitionKeyPaths.ToList()
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
                                Console.MarkupLine($"[green][bold][[NEW]][/]\t{r.Result.Resource}[/]");
                            }
                        }
                    )
            );
        }
        await Task.WhenAll(tasks);

        await database.ReplaceThroughputAsync(400);
    }
}