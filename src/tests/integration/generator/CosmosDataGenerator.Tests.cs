// Copyright (c) Microsoft Corporation. All rights reserved.
namespace Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Generator.Tests.Integration;

public sealed class CosmosDataGeneratorTests : IAsyncLifetime
{
    private readonly string testDatabaseName = $"cosmicworks-database-integration-{DateTimeOffset.UtcNow:s}";

    private readonly string testProductsContainerName = $"cosmicworks-products-container-integration-{DateTimeOffset.UtcNow:s}";

    private readonly string testEmployeesContainerName = $"cosmicworks-employees-container-integration-{DateTimeOffset.UtcNow:s}";

    private readonly EmulatorCosmosClientService clientService = new();

    [Fact]
    public async Task GetAllProductsTest()
    {
        // Arrange
        CosmosDataGenerator<Product> generator = new(
            new ProductsDataSource(),
            new CosmosDataService<Product>(clientService)
        );
        ConnectionOptions options = new()
        {
            Type = ConnectionType.ResourceOwnerPasswordCredential,
            Credential = Constants.EmulatorCredential
        };

        // Act
        await generator.GenerateAsync(
            options,
            databaseName: testDatabaseName,
            containerName: testProductsContainerName,
            disableHierarchicalPartitionKeys: true
        );

        // Assert
        CosmosClient client = clientService.GetCosmosClient(new ConnectionOptions
        {
            Type = ConnectionType.ResourceOwnerPasswordCredential,
            Credential = Constants.EmulatorCredential
        });
        Container container = client.GetContainer(testDatabaseName, testProductsContainerName);
        FeedIterator<Product> feed = container.GetItemLinqQueryable<Product>().ToFeedIterator();
        List<Product> items = [];
        while (feed.HasMoreResults)
        {
            FeedResponse<Product> page = await feed.ReadNextAsync();
            items.AddRange(page);
        }
        Assert.Equal(Constants.ExpectedProductsCount, items.Count);
    }

    [Fact]
    public async Task GetAllEmployeesTest()
    {
        // Arrange
        CosmosDataGenerator<Employee> generator = new(
            new EmployeesDataSource(),
            new CosmosDataService<Employee>(clientService)
        );
        ConnectionOptions options = new()
        {
            Type = ConnectionType.ResourceOwnerPasswordCredential,
            Credential = Constants.EmulatorCredential
        };

        // Act
        await generator.GenerateAsync(
            options,
            databaseName: testDatabaseName,
            containerName: testEmployeesContainerName,
            disableHierarchicalPartitionKeys: true
        );

        // Assert
        CosmosClient client = clientService.GetCosmosClient(new ConnectionOptions
        {
            Type = ConnectionType.ResourceOwnerPasswordCredential,
            Credential = Constants.EmulatorCredential
        });
        Container container = client.GetContainer(testDatabaseName, testEmployeesContainerName);
        FeedIterator<Employee> feed = container.GetItemLinqQueryable<Employee>().ToFeedIterator();
        List<Employee> items = [];
        while (feed.HasMoreResults)
        {
            FeedResponse<Employee> page = await feed.ReadNextAsync();
            items.AddRange(page);
        }
        Assert.Equal(Constants.ExpectedEmployeesCount, items.Count);
    }

    public Task InitializeAsync() => Task.CompletedTask;

    public async Task DisposeAsync()
    {
        ConnectionOptions options = new()
        {
            Type = ConnectionType.ResourceOwnerPasswordCredential,
            Credential = Constants.EmulatorCredential
        };

        CosmosClient client = clientService.GetCosmosClient(options);

        Database database = client.GetDatabase(testDatabaseName);

        await database.DeleteAsync();
    }
}

/// <summary>
/// Provides a CosmosClientService implementation that's specifically tuned for the emulator.
/// </summary>
internal sealed class EmulatorCosmosClientService : ICosmosClientService
{
    private CosmosClient? _client;

    public CosmosClient GetCosmosClient(ConnectionOptions options) => _client ??= new CosmosClient(
        Constants.EmulatorCredential,
        new CosmosClientOptions
        {
            SerializerOptions = new CosmosSerializationOptions
            {
                IgnoreNullValues = true,
                PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase,
            },
            ConnectionMode = ConnectionMode.Gateway,
            AllowBulkExecution = false,
            MaxRetryWaitTimeOnRateLimitedRequests = TimeSpan.FromSeconds(30),
            MaxRetryAttemptsOnRateLimitedRequests = 30,
            ServerCertificateCustomValidationCallback = (_, _, _) => true
        }
    );
}