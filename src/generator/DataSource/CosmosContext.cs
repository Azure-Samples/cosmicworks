// Copyright (c) Microsoft Corporation. All rights reserved.
namespace Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Generator.DataSource;

using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Generator.BuilderFactory;

/// <inheritdoc/>
public sealed class CosmosContext(
    ICosmosClientBuilderFactory cosmosClientBuilderFactory) : ICosmosContext
{
    private const int _databaseThroughput = 400;

    /// <inheritdoc/>
    public async Task SeedDataAsync<T>(CosmosClientBuilderFactoryOptions factoryOptions, string databaseName, ContainerProperties containerProperties, IEnumerable<T> items, Action<string> onCreated)
    {
        ArgumentNullException.ThrowIfNull(items);
        ArgumentNullException.ThrowIfNull(factoryOptions);
        ArgumentNullException.ThrowIfNull(containerProperties);

        CosmosClientBuilder clientBuilder = cosmosClientBuilderFactory.GetBuilder(factoryOptions);

        using CosmosClient client = clientBuilder
            .WithSerializerOptions(new CosmosSerializationOptions()
            {
                IgnoreNullValues = true,
                PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase,
            })
            .WithBulkExecution(true)
            .WithThrottlingRetryOptions(TimeSpan.FromSeconds(30), 30)
            .Build();

        AccountProperties accountProperties = await client.ReadAccountAsync();

        Database database = factoryOptions.UseRoleBasedAccessControl ?
        client.GetDatabase(
            id: databaseName
        ) : await client.CreateDatabaseIfNotExistsAsync(
            id: databaseName,
            throughput: _databaseThroughput
        );

        Container container = factoryOptions.UseRoleBasedAccessControl ?
        database.GetContainer(
            id: containerProperties.Id
        ) : await database.CreateContainerIfNotExistsAsync(
            containerProperties: containerProperties
        );

        if (database is not null && container is not null)
        {
            if (!factoryOptions.UseRoleBasedAccessControl)
            {
                await database.ReplaceThroughputAsync(4000);
            }

            List<Task> tasks = [];
            foreach (var item in items)
            {
                tasks.Add(
                    container.UpsertItemAsync(item)
                        .ContinueWith(response =>
                            {
                                if (response.IsCompletedSuccessfully)
                                {
                                    onCreated($"{response.Result.Resource}");
                                }
                                else if (response.Exception is not null)
                                {
                                    throw response.Exception;
                                }
                            }, TaskScheduler.Default
                        )
                );
            }
            await Task.WhenAll(tasks);

            if (!factoryOptions.UseRoleBasedAccessControl)
            {
                await database.ReplaceThroughputAsync(4000);
            }
        }
    }
}