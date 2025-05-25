// Copyright (c) Microsoft Corporation. All rights reserved.
namespace Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Generator.Tests.Integration;

public class CosmosDataGeneratorTests
{
    [Fact]
    public async Task GetAllEmployeesTest()
    {
        // Arrange
        CosmosDataGenerator<Product> generator = new(
            new ProductsDataSource(),
            new CosmosDataService<Product>(
                new EmulatorCosmosClientService()
            )
        );

        ConnectionOptions options = new()
        {
            Type = ConnectionType.ResourceOwnerPasswordCredential,
            Credential = Constants.EmulatorCredential
        };

        await generator.GenerateAsync(
            options: options,
            databaseName: "cosmicworks",
            containerName: "products",
            count: 1000,
            disableHierarchicalPartitionKeys: false,
            onItemCreate: (_) => { }
        );
    }
}
