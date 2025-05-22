// Copyright (c) Microsoft Corporation. All rights reserved.
namespace Microsoft.Samples.Cosmos.NoSQL.CosmicWorks.Generator.BuilderFactory;

/// <inheritdoc/>
public class CosmosClientBuilderFactory : ICosmosClientBuilderFactory
{
    private readonly string emulatorConnectionString = "AccountEndpoint=https://localhost:8081/;AccountKey=C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
    private readonly TokenCredential roleBasedAccessControlCredential = new DefaultAzureCredential();

    /// <inheritdoc/>
    public CosmosClientBuilder GetBuilder(CosmosClientBuilderFactoryOptions options) => (options) switch
    {
        { UseEmulator: true, UseRoleBasedAccessControl: false } => new CosmosClientBuilder(emulatorConnectionString),
        { UseEmulator: false, UseRoleBasedAccessControl: true } => new CosmosClientBuilder(options.Endpoint, roleBasedAccessControlCredential),
        { UseEmulator: false, UseRoleBasedAccessControl: false } => new CosmosClientBuilder(options.ConnectionString),
        _ => throw new NotImplementedException()
    };
}