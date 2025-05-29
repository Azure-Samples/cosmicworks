// Copyright (c) Microsoft Corporation. All rights reserved.
namespace Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Tool.Extensions;

internal static class CommandAppExtensions
{
    public static void ConfigureCommands(this CommandApp app)
    {
        app.Configure(configuration =>
        {
            configuration.AddBranch<GenerateSettings>("generate", branch =>
            {
                branch.AddCommand<GenerateEmployeesCommand>("employees")
                    .WithDescription("Generate fictitious employee data for Azure Cosmos DB for NoSQL.")
                    .WithExample("generate", "employees", "--endpoint", "<azure-cosmos-db-nosql-account-endpoint>")
                    .WithExample("generate", "employees", "--endpoint", "<azure-cosmos-db-nosql-account-endpoint>", "--quantity", "100")
                    .WithExample("generate", "products", "--endpoint", "<azure-cosmos-db-nosql-account-endpoint>", "--database-name", "human-resources", "--container-name", "people")
                    .WithExample("generate", "employees", "--connection-string", "<azure-cosmos-db-nosql-connection-string>", "--disable-hierarchical-partition-keys")
                    .WithExample("generate", "employees", "--emulator");

                branch.AddCommand<GenerateProductsCommand>("products")
                    .WithDescription("Generate fictitious product data for Azure Cosmos DB for NoSQL.")
                    .WithExample("generate", "products", "--endpoint", "<azure-cosmos-db-nosql-account-endpoint>")
                    .WithExample("generate", "products", "--endpoint", "<azure-cosmos-db-nosql-account-endpoint>", "--quantity", "100")
                    .WithExample("generate", "products", "--endpoint", "<azure-cosmos-db-nosql-account-endpoint>", "--database-name", "assets", "--container-name", "inventory")
                    .WithExample("generate", "products", "--connection-string", "<azure-cosmos-db-nosql-connection-string>", "--disable-hierarchical-partition-keys")
                    .WithExample("generate", "products", "--emulator");
            });
        });
    }
}