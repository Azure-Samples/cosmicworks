// Copyright (c) Microsoft Corporation. All rights reserved.
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Generator.Services;

/// <inheritdoc />
public sealed class CosmosDataService<T>(
    ICosmosClientService cosmosClientService
) : ICosmosDataService<T> where T : IItem
{
    private const int fixedDatabaseThroughput = 400;

    /// <inheritdoc />
    public async Task<bool> ProvisionResourcesAsync(ConnectionOptions options, string databaseName, string containerName, string[] partitionKeys, bool disableHierarchicalPartitionKeys)
    {
        ArgumentNullException.ThrowIfNull(options);
        ArgumentNullException.ThrowIfNull(partitionKeys);
        ArgumentException.ThrowIfNullOrWhiteSpace(databaseName);
        ArgumentException.ThrowIfNullOrWhiteSpace(containerName);

        bool serverless;
        try
        {
            CosmosClient client = cosmosClientService.GetCosmosClient(options);
            try
            {
                // Attemp to read the account properties to see if the account actually exists.
                AccountProperties properties = await client.ReadAccountAsync();

                Database database = client.GetDatabase(databaseName);
                try
                {
                    await database.ReadThroughputAsync();
                    // The account is not serverless since we were able to read throughput.
                    // The database already exists.
                    serverless = false;
                }
                catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.InternalServerError)
                {
                    // This is likely due to the account being the Docker emulator, which does not support reading throughput.
                    // But, the database already exists.
                    serverless = true;
                }
                catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.BadRequest)
                {
                    // The account is serverless, so we can't read throughput.
                    // But, the database already exists.
                    serverless = true;
                }
                catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
                {
                    // The database does not exist, so we need to create it.
                    try
                    {
                        // Test if the account is provisioned or serverless with a request.
                        database = await client.CreateDatabaseIfNotExistsAsync(databaseName, throughput: fixedDatabaseThroughput);
                        // The account is not serverless since we were able to set throughput.
                        serverless = false;
                    }
                    catch (CosmosException cex) when (cex.StatusCode == HttpStatusCode.BadRequest)
                    {
                        // The account is serverless, so we can't set throughput.
                        database = await client.CreateDatabaseIfNotExistsAsync(databaseName);
                        serverless = true;
                    }
                }
                try
                {
                    // Attempt to create the container if it doesn't already exist.
                    ContainerProperties containerProperties = GenerateContainerProperties(containerName, partitionKeys, disableHierarchicalPartitionKeys);
                    ThroughputProperties? throughputProperties = serverless ? null : ThroughputProperties.CreateManualThroughput(fixedDatabaseThroughput);
                    Container container = await database.CreateContainerIfNotExistsAsync(containerProperties, throughputProperties);
                }
                catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.BadRequest)
                {
                    // Creating a container with hierarchical partition keys is not supported for the account type.
                    throw new ClientException(
                        "This account type does not support hierarchical partition keys. Please use a different account type or partition key type.",
                        ex
                    );
                }
            }
            catch (HttpRequestException ex)
            {
                throw new ClientException(
                    "The account endpoint specified in the connection string was not found. Please check the endpoint and try again.",
                    ex
                );
            }
            catch (FormatException ex)
            {
                throw new ClientException(
                    "The key used in the connection string is not in the correct format. Please check the key and try again.",
                    ex
                );
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new ClientException(
                    "The key used in the connection string is not authorized to access the account. Please check the key and try again.",
                    ex
                );
            }
        }
        catch (FormatException ex)
        {
            throw new ClientException(
                "The connection string is not in the correct format. Please check the connection string and try again.",
                ex
            );
        }
        return serverless;
    }

    private static ContainerProperties GenerateContainerProperties(string containerName, string[] partitionKeys, bool disableHierarchicalPartitionKeys)
    {
        IndexingPolicy indexingPolicy = new()
        {
            Automatic = true,
            IndexingMode = IndexingMode.Consistent
        };
        indexingPolicy.IncludedPaths.Add(new IncludedPath { Path = "/*" });
        indexingPolicy.ExcludedPaths.Add(new ExcludedPath { Path = "/\"_etag\"/?" });

        for (int i = 2; i < partitionKeys.Length; i++)
        {
            indexingPolicy.CompositeIndexes.Add(
                [..partitionKeys[..i].Select(key => new CompositePath
                {
                    Path = $"/{key.ToLowerInvariant()}",
                    Order = CompositePathSortOrder.Ascending
                })]
            );
        }

        IReadOnlyList<string> partitionKeyPaths = [..
            (partitionKeys.Length > 0 ? partitionKeys : ["id"])
                .Select(JsonNamingPolicy.CamelCase.ConvertName)
                .Select(key => $"/{key}")
        ];

        ContainerProperties containerProperties = new()
        {
            Id = containerName,
            IndexingPolicy = indexingPolicy
        };

        if (disableHierarchicalPartitionKeys)
        {
            containerProperties.PartitionKeyPath = partitionKeyPaths[0];
        }
        else
        {
            containerProperties.PartitionKeyPaths = partitionKeyPaths;
        }

        return containerProperties;
    }

    /// <inheritdoc />
    public async Task SeedDataAsync(ConnectionOptions options, string databaseName, string containerName, IEnumerable<T> items, Action<T> onCreated)
    {
        ArgumentNullException.ThrowIfNull(options);
        ArgumentNullException.ThrowIfNull(items);
        ArgumentNullException.ThrowIfNull(onCreated);
        ArgumentException.ThrowIfNullOrWhiteSpace(databaseName);
        ArgumentException.ThrowIfNullOrWhiteSpace(containerName);

        CosmosClient client = cosmosClientService.GetCosmosClient(options);

        Container container = client.GetContainer(databaseName, containerName);

        if (container is not null)
        {
            List<Task> tasks = [];
            foreach (var item in items)
            {
                tasks.Add(
                    container.UpsertItemAsync(item)
                        .ContinueWith(response =>
                            {
                                if (response.IsCompletedSuccessfully)
                                {
                                    onCreated(response.Result.Resource);
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
        }
    }

    /// <inheritdoc />
    public async Task UpdateThroughputAsync(ConnectionOptions options, string databaseName, string containerName, int[] throughputs)
    {
        CosmosClient client = cosmosClientService.GetCosmosClient(options);

        Database database = client.GetDatabase(databaseName);
        Container container = database.GetContainer(containerName);

        foreach (int throughput in throughputs.OrderByDescending(t => t))
        {
            try
            {
                await database.ReplaceThroughputAsync(throughput);
                // Throughput was set successfully for the database;
                break;
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                // Throughput is not configured at the database level, so try to set it at the container level.
                try
                {
                    await container.ReplaceThroughputAsync(throughput);
                    // Throughput was set successfully for the container;
                    break;
                }
                catch (CosmosException cex) when (cex.StatusCode == HttpStatusCode.BadRequest)
                {
                    // Container throughput may be too high for this account type. Try the next value.
                    // Or, throughput may not be settable due to a container autoscale configuration.
                    break;
                }
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.BadRequest)
            {
                // Database throughput may be too high for this account type. Try the next value.
                // Or, throughput may not be settable due to a databse autoscale configuration.
            }
        }
    }
}